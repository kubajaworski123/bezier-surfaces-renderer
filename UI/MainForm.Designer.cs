namespace Projekt2GK.UI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            helpMenuItem = new ToolStripMenuItem();
            userManualMenuItem = new ToolStripMenuItem();
            technicalDetailsMenuItem = new ToolStripMenuItem();
            canvas = new RenderCanvas();
            controlPanel = new Panel();
            sceneGroup = new GroupBox();
            stepsLabel = new Label();
            stepsTrackBar = new TrackBar();
            bezierPolygonCheckBox = new CheckBox();
            triangleMeshCheckBox = new CheckBox();
            filledTrianglesCheckBox = new CheckBox();
            loadControlPointsButton = new Button();
            resetOrientationButton = new Button();
            lightingGroup = new GroupBox();
            kdLabel = new Label();
            kdTrackBar = new TrackBar();
            ksLabel = new Label();
            ksTrackBar = new TrackBar();
            mLabel = new Label();
            mTrackBar = new TrackBar();
            lightZLabel = new Label();
            lightZTrackBar = new TrackBar();
            lightColorButton = new Button();
            lightColorPreview = new Panel();
            animateLightCheckBox = new CheckBox();
            resetLightCurveButton = new Button();
            surfaceGroup = new GroupBox();
            surfaceLabel = new Label();
            surfaceComboBox = new ComboBox();
            solidColorRadioButton = new RadioButton();
            objectColorButton = new Button();
            textureRadioButton = new RadioButton();
            loadTextureButton = new Button();
            normalMapCheckBox = new CheckBox();
            loadNormalMapButton = new Button();
            menuStrip.SuspendLayout();
            controlPanel.SuspendLayout();
            sceneGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)stepsTrackBar).BeginInit();
            lightingGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)kdTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ksTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lightZTrackBar).BeginInit();
            surfaceGroup.SuspendLayout();
            SuspendLayout();
            //
            // menuStrip
            //
            menuStrip.Items.AddRange(new ToolStripItem[] { helpMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1100, 24);
            menuStrip.TabIndex = 0;
            //
            // helpMenuItem
            //
            helpMenuItem.DropDownItems.AddRange(new ToolStripItem[] { userManualMenuItem, technicalDetailsMenuItem });
            helpMenuItem.Name = "helpMenuItem";
            helpMenuItem.Text = "Help";
            //
            // userManualMenuItem
            //
            userManualMenuItem.Name = "userManualMenuItem";
            userManualMenuItem.Text = "User manual";
            userManualMenuItem.Click += UserManualMenuItem_Click;
            //
            // technicalDetailsMenuItem
            //
            technicalDetailsMenuItem.Name = "technicalDetailsMenuItem";
            technicalDetailsMenuItem.Text = "Technical details";
            technicalDetailsMenuItem.Click += TechnicalDetailsMenuItem_Click;
            //
            // canvas
            //
            canvas.BackColor = Color.White;
            canvas.Location = new Point(0, 24);
            canvas.Name = "canvas";
            canvas.Size = new Size(800, 800);
            canvas.TabIndex = 1;
            canvas.TabStop = false;
            canvas.MouseDown += Canvas_MouseDown;
            canvas.MouseMove += Canvas_MouseMove;
            canvas.MouseUp += Canvas_MouseUp;
            //
            // controlPanel
            //
            controlPanel.Controls.Add(sceneGroup);
            controlPanel.Controls.Add(lightingGroup);
            controlPanel.Controls.Add(surfaceGroup);
            controlPanel.Location = new Point(800, 24);
            controlPanel.Name = "controlPanel";
            controlPanel.Size = new Size(300, 800);
            controlPanel.TabIndex = 2;
            //
            // sceneGroup
            //
            sceneGroup.Controls.Add(stepsLabel);
            sceneGroup.Controls.Add(stepsTrackBar);
            sceneGroup.Controls.Add(bezierPolygonCheckBox);
            sceneGroup.Controls.Add(triangleMeshCheckBox);
            sceneGroup.Controls.Add(filledTrianglesCheckBox);
            sceneGroup.Controls.Add(loadControlPointsButton);
            sceneGroup.Controls.Add(resetOrientationButton);
            sceneGroup.Location = new Point(12, 8);
            sceneGroup.Name = "sceneGroup";
            sceneGroup.Size = new Size(276, 190);
            sceneGroup.TabIndex = 0;
            sceneGroup.TabStop = false;
            sceneGroup.Text = "Scene";
            //
            // stepsLabel
            //
            stepsLabel.AutoSize = true;
            stepsLabel.Location = new Point(10, 22);
            stepsLabel.Name = "stepsLabel";
            stepsLabel.Text = "Triangulation steps";
            //
            // stepsTrackBar
            //
            stepsTrackBar.AutoSize = false;
            stepsTrackBar.Location = new Point(6, 40);
            stepsTrackBar.Maximum = 60;
            stepsTrackBar.Minimum = 1;
            stepsTrackBar.Name = "stepsTrackBar";
            stepsTrackBar.Size = new Size(264, 30);
            stepsTrackBar.TabIndex = 0;
            stepsTrackBar.TickStyle = TickStyle.None;
            stepsTrackBar.Value = 15;
            stepsTrackBar.ValueChanged += TrackBar_ValueChanged;
            //
            // bezierPolygonCheckBox
            //
            bezierPolygonCheckBox.AutoSize = true;
            bezierPolygonCheckBox.Checked = true;
            bezierPolygonCheckBox.CheckState = CheckState.Checked;
            bezierPolygonCheckBox.Location = new Point(10, 75);
            bezierPolygonCheckBox.Name = "bezierPolygonCheckBox";
            bezierPolygonCheckBox.TabIndex = 1;
            bezierPolygonCheckBox.Text = "Bezier polygon";
            bezierPolygonCheckBox.CheckedChanged += DisplayOption_CheckedChanged;
            //
            // triangleMeshCheckBox
            //
            triangleMeshCheckBox.AutoSize = true;
            triangleMeshCheckBox.Location = new Point(10, 100);
            triangleMeshCheckBox.Name = "triangleMeshCheckBox";
            triangleMeshCheckBox.TabIndex = 2;
            triangleMeshCheckBox.Text = "Triangle mesh";
            triangleMeshCheckBox.CheckedChanged += DisplayOption_CheckedChanged;
            //
            // filledTrianglesCheckBox
            //
            filledTrianglesCheckBox.AutoSize = true;
            filledTrianglesCheckBox.Checked = true;
            filledTrianglesCheckBox.CheckState = CheckState.Checked;
            filledTrianglesCheckBox.Location = new Point(10, 125);
            filledTrianglesCheckBox.Name = "filledTrianglesCheckBox";
            filledTrianglesCheckBox.TabIndex = 3;
            filledTrianglesCheckBox.Text = "Filled triangles";
            filledTrianglesCheckBox.CheckedChanged += DisplayOption_CheckedChanged;
            //
            // loadControlPointsButton
            //
            loadControlPointsButton.Location = new Point(10, 152);
            loadControlPointsButton.Name = "loadControlPointsButton";
            loadControlPointsButton.Size = new Size(124, 26);
            loadControlPointsButton.TabIndex = 4;
            loadControlPointsButton.Text = "Load surfaces...";
            loadControlPointsButton.Click += LoadControlPointsButton_Click;
            //
            // resetOrientationButton
            //
            resetOrientationButton.Location = new Point(142, 152);
            resetOrientationButton.Name = "resetOrientationButton";
            resetOrientationButton.Size = new Size(124, 26);
            resetOrientationButton.TabIndex = 5;
            resetOrientationButton.Text = "Reset rotation";
            resetOrientationButton.Click += ResetOrientationButton_Click;
            //
            // lightingGroup
            //
            lightingGroup.Controls.Add(kdLabel);
            lightingGroup.Controls.Add(kdTrackBar);
            lightingGroup.Controls.Add(ksLabel);
            lightingGroup.Controls.Add(ksTrackBar);
            lightingGroup.Controls.Add(mLabel);
            lightingGroup.Controls.Add(mTrackBar);
            lightingGroup.Controls.Add(lightZLabel);
            lightingGroup.Controls.Add(lightZTrackBar);
            lightingGroup.Controls.Add(lightColorButton);
            lightingGroup.Controls.Add(lightColorPreview);
            lightingGroup.Controls.Add(animateLightCheckBox);
            lightingGroup.Controls.Add(resetLightCurveButton);
            lightingGroup.Location = new Point(12, 206);
            lightingGroup.Name = "lightingGroup";
            lightingGroup.Size = new Size(276, 290);
            lightingGroup.TabIndex = 1;
            lightingGroup.TabStop = false;
            lightingGroup.Text = "Lighting";
            //
            // kdLabel
            //
            kdLabel.AutoSize = true;
            kdLabel.Location = new Point(10, 22);
            kdLabel.Name = "kdLabel";
            kdLabel.Text = "kd";
            //
            // kdTrackBar
            //
            kdTrackBar.AutoSize = false;
            kdTrackBar.Location = new Point(6, 40);
            kdTrackBar.Maximum = 100;
            kdTrackBar.Name = "kdTrackBar";
            kdTrackBar.Size = new Size(264, 30);
            kdTrackBar.TabIndex = 0;
            kdTrackBar.TickStyle = TickStyle.None;
            kdTrackBar.Value = 50;
            kdTrackBar.ValueChanged += TrackBar_ValueChanged;
            //
            // ksLabel
            //
            ksLabel.AutoSize = true;
            ksLabel.Location = new Point(10, 72);
            ksLabel.Name = "ksLabel";
            ksLabel.Text = "ks";
            //
            // ksTrackBar
            //
            ksTrackBar.AutoSize = false;
            ksTrackBar.Location = new Point(6, 90);
            ksTrackBar.Maximum = 100;
            ksTrackBar.Name = "ksTrackBar";
            ksTrackBar.Size = new Size(264, 30);
            ksTrackBar.TabIndex = 1;
            ksTrackBar.TickStyle = TickStyle.None;
            ksTrackBar.Value = 50;
            ksTrackBar.ValueChanged += TrackBar_ValueChanged;
            //
            // mLabel
            //
            mLabel.AutoSize = true;
            mLabel.Location = new Point(10, 122);
            mLabel.Name = "mLabel";
            mLabel.Text = "m";
            //
            // mTrackBar
            //
            mTrackBar.AutoSize = false;
            mTrackBar.Location = new Point(6, 140);
            mTrackBar.Maximum = 100;
            mTrackBar.Minimum = 1;
            mTrackBar.Name = "mTrackBar";
            mTrackBar.Size = new Size(264, 30);
            mTrackBar.TabIndex = 2;
            mTrackBar.TickStyle = TickStyle.None;
            mTrackBar.Value = 20;
            mTrackBar.ValueChanged += TrackBar_ValueChanged;
            //
            // lightZLabel
            //
            lightZLabel.AutoSize = true;
            lightZLabel.Location = new Point(10, 172);
            lightZLabel.Name = "lightZLabel";
            lightZLabel.Text = "Light z";
            //
            // lightZTrackBar
            //
            lightZTrackBar.AutoSize = false;
            lightZTrackBar.Location = new Point(6, 190);
            lightZTrackBar.Maximum = 500;
            lightZTrackBar.Minimum = 10;
            lightZTrackBar.Name = "lightZTrackBar";
            lightZTrackBar.Size = new Size(264, 30);
            lightZTrackBar.TabIndex = 3;
            lightZTrackBar.TickStyle = TickStyle.None;
            lightZTrackBar.Value = 200;
            lightZTrackBar.ValueChanged += TrackBar_ValueChanged;
            //
            // lightColorButton
            //
            lightColorButton.Location = new Point(10, 224);
            lightColorButton.Name = "lightColorButton";
            lightColorButton.Size = new Size(124, 26);
            lightColorButton.TabIndex = 4;
            lightColorButton.Text = "Light color...";
            lightColorButton.Click += LightColorButton_Click;
            //
            // lightColorPreview
            //
            lightColorPreview.BackColor = Color.White;
            lightColorPreview.BorderStyle = BorderStyle.FixedSingle;
            lightColorPreview.Location = new Point(142, 226);
            lightColorPreview.Name = "lightColorPreview";
            lightColorPreview.Size = new Size(22, 22);
            //
            // resetLightCurveButton
            //
            resetLightCurveButton.Location = new Point(142, 254);
            resetLightCurveButton.Name = "resetLightCurveButton";
            resetLightCurveButton.Size = new Size(124, 26);
            resetLightCurveButton.TabIndex = 6;
            resetLightCurveButton.Text = "Reset light curve";
            resetLightCurveButton.Click += ResetLightCurveButton_Click;
            //
            // animateLightCheckBox
            //
            animateLightCheckBox.AutoSize = true;
            animateLightCheckBox.Checked = true;
            animateLightCheckBox.CheckState = CheckState.Checked;
            animateLightCheckBox.Location = new Point(10, 258);
            animateLightCheckBox.Name = "animateLightCheckBox";
            animateLightCheckBox.TabIndex = 5;
            animateLightCheckBox.Text = "Animate (Space)";
            animateLightCheckBox.CheckedChanged += AnimateLightCheckBox_CheckedChanged;
            //
            // surfaceGroup
            //
            surfaceGroup.Controls.Add(surfaceLabel);
            surfaceGroup.Controls.Add(surfaceComboBox);
            surfaceGroup.Controls.Add(solidColorRadioButton);
            surfaceGroup.Controls.Add(objectColorButton);
            surfaceGroup.Controls.Add(textureRadioButton);
            surfaceGroup.Controls.Add(loadTextureButton);
            surfaceGroup.Controls.Add(normalMapCheckBox);
            surfaceGroup.Controls.Add(loadNormalMapButton);
            surfaceGroup.Location = new Point(12, 504);
            surfaceGroup.Name = "surfaceGroup";
            surfaceGroup.Size = new Size(276, 166);
            surfaceGroup.TabIndex = 2;
            surfaceGroup.TabStop = false;
            surfaceGroup.Text = "Surface material";
            //
            // surfaceLabel
            //
            surfaceLabel.AutoSize = true;
            surfaceLabel.Location = new Point(10, 22);
            surfaceLabel.Name = "surfaceLabel";
            surfaceLabel.Text = "Edited surface:";
            //
            // surfaceComboBox
            //
            surfaceComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            surfaceComboBox.Location = new Point(10, 40);
            surfaceComboBox.Name = "surfaceComboBox";
            surfaceComboBox.Size = new Size(256, 23);
            surfaceComboBox.TabIndex = 0;
            surfaceComboBox.SelectedIndexChanged += SurfaceComboBox_SelectedIndexChanged;
            //
            // solidColorRadioButton
            //
            solidColorRadioButton.AutoSize = true;
            solidColorRadioButton.Checked = true;
            solidColorRadioButton.Location = new Point(10, 74);
            solidColorRadioButton.Name = "solidColorRadioButton";
            solidColorRadioButton.TabIndex = 1;
            solidColorRadioButton.TabStop = true;
            solidColorRadioButton.Text = "Solid color";
            //
            // objectColorButton
            //
            objectColorButton.Location = new Point(142, 70);
            objectColorButton.Name = "objectColorButton";
            objectColorButton.Size = new Size(124, 26);
            objectColorButton.TabIndex = 2;
            objectColorButton.Text = "Choose color...";
            objectColorButton.Click += ObjectColorButton_Click;
            //
            // textureRadioButton
            //
            textureRadioButton.AutoSize = true;
            textureRadioButton.Location = new Point(10, 104);
            textureRadioButton.Name = "textureRadioButton";
            textureRadioButton.TabIndex = 3;
            textureRadioButton.Text = "Texture";
            textureRadioButton.CheckedChanged += ColorModeRadioButton_CheckedChanged;
            //
            // loadTextureButton
            //
            loadTextureButton.Location = new Point(142, 100);
            loadTextureButton.Name = "loadTextureButton";
            loadTextureButton.Size = new Size(124, 26);
            loadTextureButton.TabIndex = 4;
            loadTextureButton.Text = "Load texture...";
            loadTextureButton.Click += LoadTextureButton_Click;
            //
            // normalMapCheckBox
            //
            normalMapCheckBox.AutoSize = true;
            normalMapCheckBox.Location = new Point(10, 134);
            normalMapCheckBox.Name = "normalMapCheckBox";
            normalMapCheckBox.TabIndex = 5;
            normalMapCheckBox.Text = "Normal map";
            normalMapCheckBox.CheckedChanged += NormalMapCheckBox_CheckedChanged;
            //
            // loadNormalMapButton
            //
            loadNormalMapButton.Location = new Point(142, 130);
            loadNormalMapButton.Name = "loadNormalMapButton";
            loadNormalMapButton.Size = new Size(124, 26);
            loadNormalMapButton.TabIndex = 6;
            loadNormalMapButton.Text = "Load normal map...";
            loadNormalMapButton.Click += LoadNormalMapButton_Click;
            //
            // MainForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 824);
            Controls.Add(canvas);
            Controls.Add(controlPanel);
            Controls.Add(menuStrip);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MainMenuStrip = menuStrip;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bezier surfaces";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            controlPanel.ResumeLayout(false);
            sceneGroup.ResumeLayout(false);
            sceneGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)stepsTrackBar).EndInit();
            lightingGroup.ResumeLayout(false);
            lightingGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)kdTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)ksTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)mTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)lightZTrackBar).EndInit();
            surfaceGroup.ResumeLayout(false);
            surfaceGroup.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem helpMenuItem;
        private ToolStripMenuItem userManualMenuItem;
        private ToolStripMenuItem technicalDetailsMenuItem;
        private RenderCanvas canvas;
        private Panel controlPanel;
        private GroupBox sceneGroup;
        private Label stepsLabel;
        private TrackBar stepsTrackBar;
        private CheckBox bezierPolygonCheckBox;
        private CheckBox triangleMeshCheckBox;
        private CheckBox filledTrianglesCheckBox;
        private Button loadControlPointsButton;
        private Button resetOrientationButton;
        private GroupBox lightingGroup;
        private Label kdLabel;
        private TrackBar kdTrackBar;
        private Label ksLabel;
        private TrackBar ksTrackBar;
        private Label mLabel;
        private TrackBar mTrackBar;
        private Label lightZLabel;
        private TrackBar lightZTrackBar;
        private Button lightColorButton;
        private Panel lightColorPreview;
        private CheckBox animateLightCheckBox;
        private Button resetLightCurveButton;
        private GroupBox surfaceGroup;
        private Label surfaceLabel;
        private ComboBox surfaceComboBox;
        private RadioButton solidColorRadioButton;
        private Button objectColorButton;
        private RadioButton textureRadioButton;
        private Button loadTextureButton;
        private CheckBox normalMapCheckBox;
        private Button loadNormalMapButton;
    }
}
