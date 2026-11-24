using System.Numerics;
using System.Runtime.InteropServices;

namespace Projekt2GK.Rendering.Filling
{
    /// <summary>
    /// Wypełnianie wielokąta algorytmem scan-linii z sortowaniem kubełkowym krawędzi.
    ///
    /// ET (tablica krawędzi): kubełek dla każdej scanlinii; krawędź trafia do kubełka pierwszej
    /// scanlinii, którą przecina. AET (lista aktywnych krawędzi): krawędzie przecinające bieżącą
    /// scanlinię, posortowane po x. Dla każdej scanlinii: dołącz krawędzie z kubełka do AET,
    /// usuń krawędzie, które już się skończyły, posortuj AET po x, wypełnij piksele między
    /// kolejnymi parami krawędzi, przesuń x każdej krawędzi o 1/m.
    ///
    /// Reguła przynależności pikseli: piksel (x, y) należy do wielokąta, gdy jego środek
    /// (x + 0.5, y + 0.5) leży wewnątrz, przy czym krawędzie górne i lewe są włączane,
    /// a dolne i prawe nie. Dzięki temu sąsiednie trójkąty siatki nie mają między sobą szczelin
    /// i żaden piksel nie jest rysowany dwa razy.
    /// </summary>
    public sealed class EdgeBucketFiller : IPolygonFiller
    {
        private struct Edge
        {
            /// <summary>Ostatnia scanlinia, którą przecina krawędź.</summary>
            public int LastScanline;

            /// <summary>x przecięcia krawędzi ze środkiem bieżącej scanlinii.</summary>
            public float X;

            /// <summary>Przyrost x przy przejściu do następnej scanlinii (1/m = dx/dy).</summary>
            public float InverseSlope;
        }

        private static readonly Comparison<Edge> ByX = (a, b) => a.X.CompareTo(b.X);

        // ET: kubełki są tworzone raz i używane ponownie, żeby nie alokować pamięci dla każdego trójkąta.
        private List<Edge>[] buckets = [];
        private readonly List<Edge> activeEdges = [];

        public void Fill(ReadOnlySpan<Vector2> vertices, int width, int height, SpanCallback onSpan)
        {
            EnsureBuckets(height);

            // 1. Budowa ET: każdą niepoziomą krawędź wkładamy do kubełka jej pierwszej scanlinii.
            int firstScanline = int.MaxValue;
            int lastScanline = int.MinValue;
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector2 p = vertices[i];
                Vector2 q = vertices[(i + 1) % vertices.Length];
                if (p.Y == q.Y)
                    continue; // krawędź pozioma nie przecina środka żadnej scanlinii w sposób jednoznaczny

                Vector2 top = p.Y < q.Y ? p : q;
                Vector2 bottom = p.Y < q.Y ? q : p;

                // Scanlinie y, dla których top.Y ≤ y + 0.5 < bottom.Y, przyciętych do obszaru rysowania.
                int first = Math.Max(FirstPixelFrom(top.Y), 0);
                int last = Math.Min(FirstPixelFrom(bottom.Y) - 1, height - 1);
                if (first > last)
                    continue;

                float inverseSlope = (bottom.X - top.X) / (bottom.Y - top.Y);
                buckets[first].Add(new Edge
                {
                    LastScanline = last,
                    X = top.X + (first + 0.5f - top.Y) * inverseSlope,
                    InverseSlope = inverseSlope,
                });

                firstScanline = Math.Min(firstScanline, first);
                lastScanline = Math.Max(lastScanline, last);
            }

            // 2. Przetwarzanie scanlinii z użyciem AET.
            activeEdges.Clear();
            for (int y = firstScanline; y <= lastScanline; y++)
            {
                List<Edge> bucket = buckets[y];
                if (bucket.Count > 0)
                {
                    activeEdges.AddRange(bucket);
                    bucket.Clear();
                }

                RemoveFinishedEdges(y);
                activeEdges.Sort(ByX);

                Span<Edge> aet = CollectionsMarshal.AsSpan(activeEdges);
                for (int i = 0; i + 1 < aet.Length; i += 2)
                {
                    // Piksele, których środek x + 0.5 leży w [xLeft, xRight).
                    int xStart = Math.Max(FirstPixelFrom(aet[i].X), 0);
                    int xEnd = Math.Min(FirstPixelFrom(aet[i + 1].X), width);
                    if (xStart < xEnd)
                        onSpan(y, xStart, xEnd);
                }

                for (int i = 0; i < aet.Length; i++)
                    aet[i].X += aet[i].InverseSlope;
            }
        }

        /// <summary>Usuwa z AET krawędzie, które kończą się przed scanlinią y.</summary>
        private void RemoveFinishedEdges(int y)
        {
            for (int i = activeEdges.Count - 1; i >= 0; i--)
            {
                if (activeEdges[i].LastScanline < y)
                    activeEdges.RemoveAt(i);
            }
        }

        /// <summary>
        /// Pierwszy indeks piksela k, którego środek (k + 0.5) jest ≥ coordinate.
        /// Używane zarówno dla osi y (scanlinie), jak i x.
        /// </summary>
        private static int FirstPixelFrom(float coordinate) => (int)MathF.Ceiling(coordinate - 0.5f);

        private void EnsureBuckets(int height)
        {
            if (buckets.Length == height)
                return;

            buckets = new List<Edge>[height];
            for (int i = 0; i < height; i++)
                buckets[i] = [];
        }
    }
}
