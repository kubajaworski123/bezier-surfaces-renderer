using System.Numerics;

namespace Projekt2GK.Rendering.Filling
{
    /// <summary>
    /// Odcinek scanlinii do wypełnienia: piksele (x, y) dla xStart ≤ x &lt; xEnd.
    /// </summary>
    public delegate void SpanCallback(int y, int xStart, int xEnd);

    /// <summary>
    /// Algorytm wypełniania wielokąta (wzorzec Strategy). Wyznacza tylko, które piksele należą
    /// do wielokąta - o ich kolorze decyduje wywołujący, w <see cref="SpanCallback"/>.
    /// </summary>
    public interface IPolygonFiller
    {
        /// <param name="vertices">Wierzchołki wielokąta we współrzędnych ekranu (mogą być ułamkowe).</param>
        /// <param name="width">Szerokość obszaru rysowania - piksele poza nim są pomijane.</param>
        /// <param name="height">Wysokość obszaru rysowania.</param>
        void Fill(ReadOnlySpan<Vector2> vertices, int width, int height, SpanCallback onSpan);
    }
}
