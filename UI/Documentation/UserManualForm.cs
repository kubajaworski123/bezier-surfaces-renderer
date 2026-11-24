namespace Projekt2GK.UI.Documentation
{
    public class UserManualForm : DocumentationForm
    {
        private const string Content =
            "INSTRUKCJA OBSŁUGI\r\n" +
            "\r\n" +
            "1. Sterowanie myszą\r\n" +
            "\r\n" +
            "2. Skróty klawiaturowe\r\n" +
            "\r\n" +
            "3. Panel sterowania\r\n" +
            "\r\n" +
            "4. Format pliku z powierzchniami\r\n";

        public UserManualForm() : base("User manual", Content)
        {
        }
    }
}
