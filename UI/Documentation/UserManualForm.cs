namespace Projekt2GK.UI.Documentation
{
    public class UserManualForm : DocumentationForm
    {
        private const string Content =
            """
            INSTRUKCJA OBSŁUGI

            Program wyświetla powierzchnie Béziera stopnia 3×3 oświetlone źródłem
            światła poruszającym się po zamkniętej krzywej Béziera. Okno składa się
            z obszaru rysowania (po lewej) i panelu sterowania (po prawej).


            1. Sterowanie myszą

            • Obrót sceny – wciśnij lewy przycisk myszy w dowolnym miejscu obszaru
              rysowania (poza punktami kontrolnymi krzywej światła) i przeciągnij.

            • Edycja krzywej światła – najedź na jeden z punktów kontrolnych krzywej, wciśnij lewy przycisk i przeciągnij punkt w nowe
              miejsce. Pierwszy i ostatni punkt krzywej są tym samym punktem
              (V0 = V6), więc krzywa zawsze pozostaje zamknięta.


            2. Skróty klawiaturowe

            • Spacja – zatrzymanie / wznowienie animacji ruchu światła


            3. Panel sterowania

            3.1. Scene
            • Triangulation steps (1–60, domyślnie 15) – dokładność triangulacji:
              liczba podziałów każdej powierzchni w kierunkach u i v.
              Powierzchnia składa się z 2·n² trójkątów.
            • Bezier polygon – pokazuje siatkę punktów kontrolnych powierzchni.
            • Triangle mesh – pokazuje krawędzie trójkątów siatki.
            • Filled triangles – włącza wypełnianie trójkątów z oświetleniem.
            • Load surfaces... – wczytuje powierzchnie z pliku tekstowego
            • Reset rotation – przywraca początkową orientację sceny.

            3.2. Surface material
            Ustawienia dotyczą powierzchni wybranej na liście "Edited surface".
            • Solid color – powierzchnia ma jednolity kolor
              (zmiana przyciskiem "Choose color...").
            • Texture – kolor powierzchni pobierany jest z tekstury
            • Normal map – włącza modyfikację wektorów normalnych
            Domyślnie powierzchnie mają jednolity kolor i wyłączoną mapę normalnych.

            3.3. Lighting
            • kd (0–1) – współczynnik rozpraszania (składowa rozproszona, Lambert).
            • ks (0–1) – współczynnik odbicia zwierciadlanego.
            • m (1–100) – wykładnik odbicia zwierciadlanego.
            • Light z (0,1–5) – wysokość płaszczyzny z = const, na której leży
              krzywa światła.
            • Light color... – wybór koloru światła.
            • Animate (Space) – włącza / wyłącza ruch światła po krzywej.
            • Reset light curve – przywraca domyślny kształt krzywej światła.

            4. Format pliku z powierzchniami

            Plik tekstowy, w którym każdy niepusty wiersz zawiera współrzędne jednego
            punktu kontrolnego: trzy liczby x y z oddzielone spacjami, z kropką jako
            separatorem dziesiętnym, np.:
                -1 -0.333 0.25

            Każda powierzchnia to 16 kolejnych wierszy (siatka 4×4 punktów).
            Wiersz k (k = 0..15) to punkt P[i, j], gdzie i = k div 4, j = k mod 4.
            Liczba punktów w pliku musi być wielokrotnością 16 – plik może zawierać
            dowolnie wiele powierzchni. Puste wiersze są pomijane.
            Współrzędne powinny mieścić się w sześcianie [-1, 1]³.
            W przypadku błędu program wyświetla komunikat z numerem błędnego wiersza,
            a dotychczasowe powierzchnie pozostają bez zmian.

            Przykładowe pliki w katalogu Assets:
            • surfaces.txt – domyślny plik (dwie przecinające się powierzchnie),
            • surfaces_separate.txt – powierzchnie rozłączne.
            """;

        public UserManualForm() : base("User manual", Content)
        {
        }
    }
}
