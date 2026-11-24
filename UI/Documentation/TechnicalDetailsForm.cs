namespace Projekt2GK.UI.Documentation
{
    public class TechnicalDetailsForm : DocumentationForm
    {
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
