using Projekt2GK.Geometry;
using Projekt2GK.Interaction;
using Projekt2GK.Model;
using Projekt2GK.Rendering;
using Projekt2GK.UI.Documentation;
using Projekt2GK.Utils;

namespace Projekt2GK.UI
{
    public partial class MainForm : Form
    {
        private readonly Scene scene = new();
        private readonly RenderSettings settings = new();
        private readonly SceneRenderer renderer;
        private readonly MouseController mouseController;

        /// <summary>Okres timera animacji (~60 kroków na sekundę).</summary>
        private const int AnimationIntervalMs = 16;
        private readonly System.Windows.Forms.Timer animationTimer;
        private readonly System.Diagnostics.Stopwatch animationClock = new();

        private const string DefaultTexturePath = "Assets/texture.png";
        private const string DefaultNormalMapPath = "Assets/normalmap_bricks.png";
        private readonly Texture? defaultTexture;
        private readonly NormalMap? defaultNormalMap;

        /// <summary>Ustawiana na czas programowego ustawiania kontrolek materiału.</summary>
        private bool updatingMaterialControls;

        public MainForm()
        {
            InitializeComponent();

            renderer = new SceneRenderer(scene, settings, canvas.ClientSize.Width, canvas.ClientSize.Height);
            canvas.Renderer = renderer;
            mouseController = new MouseController(
                new InteractionContext(scene, () => canvas.ClientSize, InvalidateView));

            animationTimer = new System.Windows.Forms.Timer { Interval = AnimationIntervalMs };
            animationTimer.Tick += AnimationTimer_Tick;

            FormClosed += (_, _) =>
            {
                animationTimer.Dispose();
                canvas.Renderer = null;
                renderer.Dispose();
            };

            // Domyślna tekstura i mapa normalnych. Brak plików nie blokuje programu -
            // można wtedy wczytać własne przyciskami w panelu.
            defaultTexture = TryLoadImage(Path.Combine(AppContext.BaseDirectory, DefaultTexturePath), Texture.Load);
            defaultNormalMap = TryLoadImage(Path.Combine(AppContext.BaseDirectory, DefaultNormalMapPath), NormalMap.Load);

            UpdateTrackBarLabels();
            ApplySettingsFromControls();
            TryLoadSurfaces(Path.Combine(AppContext.BaseDirectory, ControlPointsLoader.DefaultFilePath));
            UpdateAnimationState();
        }

        /// <summary>Powierzchnia wybrana w comboboxie (której dotyczą ustawienia materiału).</summary>
        private Surface? SelectedSurface =>
            surfaceComboBox.SelectedIndex >= 0 && surfaceComboBox.SelectedIndex < scene.Surfaces.Count
                ? scene.Surfaces[surfaceComboBox.SelectedIndex]
                : null;

        /// <summary>
        /// Oznacza obraz jako nieaktualny. Klatka zostanie narysowana przy najbliższym odświeżeniu
        /// płótna (<see cref="RenderCanvas"/>), raz - niezależnie od liczby wywołań.
        /// </summary>
        private void InvalidateView() => canvas.Invalidate();

        /// <summary>Przepisuje stan kontrolek panelu do ustawień renderowania.</summary>
        private void ApplySettingsFromControls()
        {
            settings.TriangulationSteps = stepsTrackBar.Value;
            settings.ShowBezierPolygon = bezierPolygonCheckBox.Checked;
            settings.ShowTriangleMesh = triangleMeshCheckBox.Checked;
            settings.ShowFilledTriangles = filledTrianglesCheckBox.Checked;
            settings.Kd = kdTrackBar.Value / 100f;
            settings.Ks = ksTrackBar.Value / 100f;
            settings.M = mTrackBar.Value;

            scene.Light.Z = lightZTrackBar.Value / 100f;
        }

        #region Animacja światła

        /// <summary>Włącza lub wyłącza animację zgodnie z checkboxem.</summary>
        private void UpdateAnimationState()
        {
            if (animateLightCheckBox.Checked)
            {
                animationClock.Restart();
                animationTimer.Start();
            }
            else
            {
                animationTimer.Stop();
            }
        }

        private void AnimationTimer_Tick(object? sender, EventArgs e)
        {
            // Czas od poprzedniego kroku - prędkość światła nie zależy od tego, jak często przychodzą tyknięcia.
            float elapsedSeconds = (float)animationClock.Elapsed.TotalSeconds;
            animationClock.Restart();

            scene.Light.Advance(elapsedSeconds);
            InvalidateView();
        }

        private void AnimateLightCheckBox_CheckedChanged(object? sender, EventArgs e) => UpdateAnimationState();

        private void ResetLightCurveButton_Click(object? sender, EventArgs e)
        {
            scene.Light.Path.Reset();
            InvalidateView();
        }

        /// <summary>Spacja zatrzymuje i wznawia animację, niezależnie od tego, która kontrolka ma fokus.</summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Space)
            {
                animateLightCheckBox.Checked = !animateLightCheckBox.Checked;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        /// <summary>Okno wyboru koloru; zwraca null, gdy użytkownik anuluje.</summary>
        private Color? PickColor(Color initial)
        {
            using var dialog = new ColorDialog { Color = initial, FullOpen = true };
            return dialog.ShowDialog(this) == DialogResult.OK ? dialog.Color : null;
        }

        /// <summary>
        /// Wczytuje powierzchnie z pliku. Przy błędzie pokazuje komunikat i zostawia poprzednią scenę.
        /// </summary>
        private void TryLoadSurfaces(string path)
        {
            IReadOnlyList<BezierSurface> surfaces;
            try
            {
                surfaces = ControlPointsLoader.Load(path);
            }
            catch (Exception ex) when (ex is IOException or FormatException or UnauthorizedAccessException)
            {
                MessageBox.Show(this, $"Nie udało się wczytać pliku:\n{path}\n\n{ex.Message}",
                    "Błąd wczytywania", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            scene.SetSurfaces(surfaces, defaultTexture, defaultNormalMap);

            surfaceComboBox.Items.Clear();
            for (int i = 0; i < surfaces.Count; i++)
                surfaceComboBox.Items.Add($"Surface {i + 1}");
            surfaceComboBox.SelectedIndex = 0;

            UpdateMaterialControls();
            InvalidateView();
        }

        #region Obsługa zdarzeń

        private void Canvas_MouseDown(object? sender, MouseEventArgs e) => mouseController.OnMouseDown(e);
        private void Canvas_MouseMove(object? sender, MouseEventArgs e) => mouseController.OnMouseMove(e);
        private void Canvas_MouseUp(object? sender, MouseEventArgs e) => mouseController.OnMouseUp(e);

        private void LoadControlPointsButton_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Load Bezier surfaces",
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
                TryLoadSurfaces(dialog.FileName);
        }

        private void ResetOrientationButton_Click(object? sender, EventArgs e)
        {
            scene.ResetOrientation();
            InvalidateView();
        }

        private void LightColorButton_Click(object? sender, EventArgs e)
        {
            if (PickColor(ColorUtils.ToColor(scene.Light.Color)) is not { } color)
                return;

            scene.Light.Color = ColorUtils.ToVector(color);
            lightColorPreview.BackColor = color;
            InvalidateView();
        }

        #region Materiał wybranej powierzchni

        /// <summary>
        /// Ustawia kontrolki materiału zgodnie z wybraną powierzchnią. Flaga blokuje obsługę zdarzeń
        /// wywołanych samym ustawianiem kontrolek, żeby nie nadpisywały materiału.
        /// </summary>
        private void UpdateMaterialControls()
        {
            if (SelectedSurface is not { } surface)
                return;

            updatingMaterialControls = true;
            textureRadioButton.Checked = surface.Material.UseTexture;
            solidColorRadioButton.Checked = !surface.Material.UseTexture;
            normalMapCheckBox.Checked = surface.Material.UseNormalMap;
            updatingMaterialControls = false;
        }

        private void SurfaceComboBox_SelectedIndexChanged(object? sender, EventArgs e) => UpdateMaterialControls();

        private void ColorModeRadioButton_CheckedChanged(object? sender, EventArgs e)
        {
            if (updatingMaterialControls || SelectedSurface is not { } surface)
                return;

            surface.Material.UseTexture = textureRadioButton.Checked;
            InvalidateView();
        }

        private void NormalMapCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            if (updatingMaterialControls || SelectedSurface is not { } surface)
                return;

            surface.Material.UseNormalMap = normalMapCheckBox.Checked;
            InvalidateView();
        }

        private void ObjectColorButton_Click(object? sender, EventArgs e)
        {
            if (SelectedSurface is not { } surface)
                return;

            if (PickColor(ColorUtils.ToColor(surface.Material.SolidColor.Color)) is not { } color)
                return;

            surface.Material.SolidColor = new SolidColorSource(ColorUtils.ToVector(color));
            surface.Material.UseTexture = false;
            UpdateMaterialControls();
            InvalidateView();
        }

        private void LoadTextureButton_Click(object? sender, EventArgs e)
        {
            if (SelectedSurface is not { } surface || PickImageFile("Load texture") is not { } path)
                return;

            if (TryLoadImage(path, Texture.Load) is not { } texture)
                return;

            surface.Material.TextureSource = new TextureColorSource(texture);
            surface.Material.UseTexture = true;
            UpdateMaterialControls();
            InvalidateView();
        }

        private void LoadNormalMapButton_Click(object? sender, EventArgs e)
        {
            if (SelectedSurface is not { } surface || PickImageFile("Load normal map") is not { } path)
                return;

            if (TryLoadImage(path, NormalMap.Load) is not { } normalMap)
                return;

            surface.Material.NormalMap = normalMap;
            surface.Material.UseNormalMap = true;
            UpdateMaterialControls();
            InvalidateView();
        }

        /// <summary>Okno wyboru pliku obrazu; zwraca null, gdy użytkownik anuluje.</summary>
        private string? PickImageFile(string title)
        {
            using var dialog = new OpenFileDialog
            {
                Title = title,
                Filter = "Images (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|All files (*.*)|*.*",
            };
            return dialog.ShowDialog(this) == DialogResult.OK ? dialog.FileName : null;
        }

        /// <summary>
        /// Wczytuje obraz (teksturę lub mapę normalnych). Przy błędzie pokazuje komunikat
        /// (jeśli <paramref name="showErrors"/>) i zwraca null.
        /// </summary>
        private T? TryLoadImage<T>(string path, Func<string, T> load, bool showErrors = true) where T : class
        {
            try
            {
                return load(path);
            }
            catch (Exception ex) when (ex is IOException or ArgumentException or UnauthorizedAccessException
                                           or System.Runtime.InteropServices.ExternalException)
            {
                if (showErrors)
                {
                    MessageBox.Show(this, $"Nie udało się wczytać obrazu:\n{path}\n\n{ex.Message}",
                        "Błąd wczytywania", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return null;
            }
        }

        #endregion

        private void DisplayOption_CheckedChanged(object? sender, EventArgs e)
        {
            ApplySettingsFromControls();
            InvalidateView();
        }

        private void TrackBar_ValueChanged(object? sender, EventArgs e)
        {
            UpdateTrackBarLabels();
            ApplySettingsFromControls();
            InvalidateView();
        }

        private void UpdateTrackBarLabels()
        {
            stepsLabel.Text = $"Triangulation steps: {stepsTrackBar.Value}";
            kdLabel.Text = $"kd: {kdTrackBar.Value / 100f:0.00}";
            ksLabel.Text = $"ks: {ksTrackBar.Value / 100f:0.00}";
            mLabel.Text = $"m: {mTrackBar.Value}";
            lightZLabel.Text = $"Light z: {lightZTrackBar.Value / 100f:0.00}";
        }

        private void UserManualMenuItem_Click(object? sender, EventArgs e) => new UserManualForm().Show(this);

        private void TechnicalDetailsMenuItem_Click(object? sender, EventArgs e) => new TechnicalDetailsForm().Show(this);

        #endregion
    }
}
