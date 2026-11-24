namespace Projekt2GK.UI.Documentation
{
    public class TechnicalDetailsForm : DocumentationForm
    {
        private const string Content =
            """
            OPIS TECHNICZNY

            Aplikacja jednowątkowa.

            1. Obrót sceny myszą

            Orientacja sceny przechowywana jest jako kwaternion jednostkowy.
            Punkt kursora jest rzutowany na wirtualną półkulę o promieniu
            min(szerokość, wysokość)/2 o środku w środku obszaru rysowania.
            Dla dwóch kolejnych położeń kursora p0, p1 obrót przyrostowy jest składany z dotychczasową
            orientacją i normalizowany, dzięki czemu obroty się kumulują
            bez gromadzenia błędów numerycznych.
            Z kwaternionu tworzona jest macierz obrotu, którą przekształcane są
            punkty, wektory styczne Pu, Pv i wektory normalne wierzchołków siatki.
            Obrót jest wykonywany ponownie tylko wtedy, gdy orientacja się zmieniła.

            Rzut jest prostokątny: współrzędna z jest pomijana, a początek układu
            znajduje się w środku obszaru rysowania (oś y skierowana w górę).
            Skala dobrana jest tak, by kula o promieniu √3 (opisana na sześcianie
            [−1, 1]³) mieściła się w oknie przy dowolnym obrocie.

            Obsługa myszy wykorzystuje wzorzec Stan: Idle, RotatingScene
            (obrót sceny) oraz DraggingCurvePoint (przeciąganie punktu
            kontrolnego krzywej światła).


            2. Architektura (potok renderowania)

            Klatka jest rysowana w OnPaint kontrolki RenderCanvas – zdarzenia
            (mysz, suwaki, animacja) jedynie unieważniają widok, więc kilka zmian
            między odświeżeniami daje jedno renderowanie (dla poprawy wydajności).
            Renderowanie to potok etapów implementujących IRenderStage,
            wykonywanych kolejno (etap może być wyłączony ustawieniami):
            1) Tessellation – triangulacja powierzchni, tylko gdy zmieniła się
               dokładność; dla n podziałów powstaje (n+1)² wierzchołków
               i 2n² trójkątów, a w każdym wierzchołku liczone są P(u,v),
               Pu, Pv oraz N = Pu × Pv (znormalizowany),
            2) Transform – obrót wierzchołków bieżącą macierzą orientacji,
            3) Clear – czyszczenie bufora kolorów i Z-bufora,
            4) Fill – wypełnianie trójkątów z oświetleniem,
            5) Present – skopiowanie bufora pikseli (tablica int ARGB) do Bitmapy,
            6) MeshOverlay – krawędzie siatki trójkątów,
            7) ControlPolygonOverlay – siatka punktów kontrolnych,
            8) LightOverlay – krzywa światła, jej punkty kontrolne i źródło światła.
            Linie rysowane są funkcjami biblioteki Graphics.

            Z-bufor: przy wypełnianiu dla każdego piksela interpolowana jest
            współrzędna z po obrocie; piksel jest rysowany tylko wtedy, gdy jest
            bliżej obserwatora, niż dotychczas zapisany.


            3. Wypełnianie trójkątów (sortowanie kubełkowe krawędzi)

            Każdy trójkąt jest wypełniany algorytmem scan-line z tablicą
            krawędzi (ET) w postaci kubełków indeksowanych numerem wiersza:
            • krawędzie poziome są pomijane; każda pozostała krawędź trafia do
              kubełka pierwszego wiersza, który przecina, z zapamiętanym x
              przecięcia, ostatnim wierszem i odwrotnością nachylenia 1/m,
            • dla kolejnych wierszy y krawędzie z kubełka y są dołączane do
              listy krawędzi aktywnych (AET), zakończone krawędzie są usuwane,
              a AET jest sortowana po x,
            • piksele między kolejnymi parami krawędzi są wypełniane,
            • x każdej aktywnej krawędzi zwiększa się o 1/m.
            Próbkowane są środki pikseli (x + 0.5, y + 0.5), z regułą „lewa / górna
            krawędź włącznie”, dzięki czemu sąsiednie trójkąty nie zostawiają
            szczelin ani nie rysują tych samych pikseli dwukrotnie.
            Wypełnianie jest oddzielone od cieniowania interfejsem IPolygonFiller
            (wzorzec Strategia) – dla każdego odcinka poziomego wywoływana jest
            funkcja cieniująca piksele.


            4. Model oświetlenia i interpolacja barycentryczna

            Kolor piksela:
                I = kd · IL · IO · cos(N, L) + ks · IL · IO · cosᵐ(V, R),
            gdzie IL – kolor światła, IO – kolor obiektu (jednolity lub z tekstury),
            V = [0, 0, 1], R = 2⟨N, L⟩N − L, a L to wersor od punktu do światła.
            Ujemne cosinusy są zastępowane zerem. Jeśli normalna jest zwrócona od
            obserwatora, jest odwracana (oświetlenie dwustronne). Wynik jest
            obcinany do przedziału [0, 1] dla każdej składowej.

            Wartości w pikselu nie są liczone dla wierzchołków, lecz interpolowane
            współrzędnymi barycentrycznymi (λa, λb, λc) punktu w trójkącie na ekranie:
            • λb, λc to stosunki pól (iloczynów wektorowych) podtrójkątów do pola
              trójkąta, λa = 1 − λb − λc,
            • interpolowane są: położenie 3D (do wektora L), z (Z-bufor), wektor
              normalny (normalizowany po interpolacji), parametry (u, v)
              (do tekstury i mapy normalnych) oraz Pu, Pv (do mapy normalnych),
            • współrzędne barycentryczne są liczone raz na odcinek, a dla kolejnych
              pikseli zwiększane o stały przyrost (są liniowe względem x).
            Potęga cosᵐ liczona jest szybkim potęgowaniem całkowitym.

            Źródło światła porusza się po zamkniętej krzywej Béziera stopnia 6
            (V6 = V0) na płaszczyźnie z = const. Parametr t ∈ [0, 1) rośnie
            proporcjonalnie do upływu czasu (jedno okrążenie w 8 s), niezależnie
            od częstotliwości timera. Światło nie obraca się razem ze sceną.


            5. Kolor obiektu i mapa wektorów normalnych

            Kolor obiektu i wektor normalny dobierane są osobno dla każdej
            powierzchni (wzorzec Strategia: IColorSource, INormalProvider).
            Tekstura i mapa normalnych są próbkowane w punkcie (u, v) metodą
            najbliższego piksela; oś v jest odwrócona, bo w obrazie wiersze
            rosną w dół.

            Mapa normalnych: kolor piksela (R, G, B) zamieniany jest na wektor
                Ntex = (R/255 · 2 − 1, G/255 · 2 − 1, B/255 · 2 − 1)
            (znormalizowany przy wczytywaniu). Zmodyfikowany wektor normalny to
                N = M · Ntex,   M = [Pu, Pv, Nsurface],
            gdzie kolumnami macierzy M są znormalizowane, interpolowane wektory
            styczne Pu, Pv oraz wektor normalny powierzchni. Wynik jest
            normalizowany i używany w modelu oświetlenia.
            """;

        public TechnicalDetailsForm() : base("Technical details", Content)
        {
        }
    }
}
