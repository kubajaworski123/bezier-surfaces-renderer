using System.Numerics;

namespace Projekt2GK.Rendering.Filling
{
    public delegate void SpanCallback(int y, int xStart, int xEnd);

    public interface IPolygonFiller
    {
        void Fill(ReadOnlySpan<Vector2> vertices, int width, int height, SpanCallback onSpan);
    }
}
