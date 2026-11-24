using System.ComponentModel;
using Projekt2GK.Rendering;

namespace Projekt2GK.UI
{
    /// <summary>
    /// Płótno, które renderuje scenę w momencie rysowania okna (WM_PAINT).
    ///
    /// Zmiany stanu (mysz, suwaki, animacja) wywołują tylko Invalidate(). Windows łączy wszystkie
    /// unieważnienia od ostatniego rysowania w jeden komunikat WM_PAINT, więc niezależnie od liczby
    /// zdarzeń powstaje co najwyżej jedna klatka na odświeżenie okna, zawsze z najnowszym stanem sceny.
    /// </summary>
    public class RenderCanvas : Control
    {
        public RenderCanvas()
        {
            // Całe płótno rysujemy sami jednym DrawImage: bez czyszczenia tła (Opaque) nie ma migotania,
            // więc dodatkowe podwójne buforowanie nie jest potrzebne.
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
        }

        /// <summary>Renderer, którego klatki są wyświetlane; null - płótno jest puste.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SceneRenderer? Renderer { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Renderer is null)
            {
                e.Graphics.Clear(BackColor);
                return;
            }

            Renderer.Render();
            e.Graphics.DrawImageUnscaled(Renderer.Output, 0, 0);
        }
    }
}
