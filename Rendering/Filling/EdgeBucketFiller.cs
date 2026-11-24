using System.Numerics;
using System.Runtime.InteropServices;

namespace Projekt2GK.Rendering.Filling
{
    public sealed class EdgeBucketFiller : IPolygonFiller
    {
        private struct Edge
        {
            public int LastScanline;

            public float X;

            public float InverseSlope;
        }

        private static readonly Comparison<Edge> ByX = (a, b) => a.X.CompareTo(b.X);

        private List<Edge>[] buckets = [];
        private readonly List<Edge> activeEdges = [];

        public void Fill(ReadOnlySpan<Vector2> vertices, int width, int height, SpanCallback onSpan)
        {
            EnsureBuckets(height);

            int firstScanline = int.MaxValue;
            int lastScanline = int.MinValue;
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector2 p = vertices[i];
                Vector2 q = vertices[(i + 1) % vertices.Length];
                if (p.Y == q.Y)
                    continue;

                Vector2 top = p.Y < q.Y ? p : q;
                Vector2 bottom = p.Y < q.Y ? q : p;

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
                    int xStart = Math.Max(FirstPixelFrom(aet[i].X), 0);
                    int xEnd = Math.Min(FirstPixelFrom(aet[i + 1].X), width);
                    if (xStart < xEnd)
                        onSpan(y, xStart, xEnd);
                }

                for (int i = 0; i < aet.Length; i++)
                    aet[i].X += aet[i].InverseSlope;
            }
        }

        private void RemoveFinishedEdges(int y)
        {
            for (int i = activeEdges.Count - 1; i >= 0; i--)
            {
                if (activeEdges[i].LastScanline < y)
                    activeEdges.RemoveAt(i);
            }
        }

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
