using System.Numerics;
using Projekt2GK.Geometry;
using Projekt2GK.Model;

namespace Projekt2GK.Rendering.Stages
{
    /// <summary>
    /// Rysuje wielobok Béziera na gotowej bitmapie: punkty kontrolne połączone wzdłuż wierszy i kolumn siatki 4x4.
    /// </summary>
    public class ControlPolygonOverlayStage : IRenderStage
    {
        private static readonly Color LineColor = Color.FromArgb(0x60, 0x60, 0x60);
        private static readonly Color PointColor = Color.FromArgb(0xD3, 0x2F, 0x2F);
        private const float PointSize = 5f;

        public bool IsEnabled(RenderContext context) => context.Settings.ShowBezierPolygon;

        public void Execute(RenderContext context)
        {
            const int n = BezierSurface.PointsPerSide;
            Matrix4x4 rotation = context.Scene.RotationMatrix;
            var screen = new PointF[n, n];

            using Graphics graphics = Graphics.FromImage(context.Output);
            using var linePen = new Pen(LineColor);
            using var pointBrush = new SolidBrush(PointColor);

            foreach (Surface surface in context.Scene.Surfaces)
            {
                // 16 punktów obracamy na bieżąco - to tańsze niż przechowywanie ich kopii.
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        screen[i, j] = context.ToScreenPoint(Vector3.Transform(surface.Bezier.GetControlPoint(i, j), rotation));

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i + 1 < n)
                            graphics.DrawLine(linePen, screen[i, j], screen[i + 1, j]);
                        if (j + 1 < n)
                            graphics.DrawLine(linePen, screen[i, j], screen[i, j + 1]);
                    }
                }

                foreach (PointF point in screen)
                    graphics.FillRectangle(pointBrush, point.X - PointSize / 2, point.Y - PointSize / 2, PointSize, PointSize);
            }
        }
    }
}
