namespace Projekt2GK.UI.Documentation
{
    /// <summary>
    /// Wspólne okno do wyświetlania tekstu dokumentacji (tylko do odczytu).
    /// </summary>
    public class DocumentationForm : Form
    {
        private readonly TextBox contentTextBox;

        protected DocumentationForm(string title, string content)
        {
            Text = title;
            ClientSize = new Size(640, 560);
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            ShowInTaskbar = false;

            contentTextBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = SystemColors.Window,
                Font = new Font("Segoe UI", 10f),
                Text = content,
            };
            Controls.Add(contentTextBox);

            // Bez tego cały tekst byłby zaznaczony po otwarciu okna.
            Shown += (_, _) => contentTextBox.Select(0, 0);
        }
    }
}
