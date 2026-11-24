namespace Projekt2GK.UI.Documentation
{
    /// <summary>
    /// Opis techniczny rozwiązania, w szczególności obrotu sceny myszą (arcball).
    /// </summary>
    public class TechnicalDetailsForm : DocumentationForm
    {
        // TODO: uzupełnić treść po zaimplementowaniu funkcjonalności.
        private const string Content =
            "OPIS TECHNICZNY\r\n" +
            "\r\n" +
            "1. Obrót sceny myszą (arcball)\r\n" +
            "\r\n" +
            "2. Architektura (potok renderowania)\r\n" +
            "\r\n" +
            "3. Wypełnianie trójkątów (sortowanie kubełkowe krawędzi)\r\n" +
            "\r\n" +
            "4. Model oświetlenia i interpolacja barycentryczna\r\n" +
            "\r\n" +
            "5. Mapa wektorów normalnych\r\n";

        public TechnicalDetailsForm() : base("Technical details", Content)
        {
        }
    }
}
