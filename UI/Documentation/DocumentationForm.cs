namespace Projekt2GK.UI.Documentation
{
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
                Text = content.ReplaceLineEndings("\r\n"),
            };
            Controls.Add(contentTextBox);

            Shown += (_, _) => contentTextBox.Select(0, 0);
        }
    }
}
