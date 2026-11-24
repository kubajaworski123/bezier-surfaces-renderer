using System.Drawing.Drawing2D;
using System.Numerics;
using Projekt2GK.Model;

namespace Projekt2GK.Rendering.Stages
{
    /// <summary>
    /// Rysuje krzywą Béziera, po której porusza się światło (wielobok kontrolny, punkty kontrolne,
    /// samą krzywą) oraz aktualne położenie światła na krzywej.
    /// Światło jest w układzie obserwatora, więc nie jest obracane razem ze sceną.
    /// </summary>
    public class LightOverlayStage : IRenderStage
    {
        private const int CurveSamples = 120;
        private const float ControlPointSize = 7f;
        private const float LightRadius = 6f;

        private static readonly Color CurveColor = Color.FromArgb(0xE6, 0x8A, 0x00);
        private static readonly Color PolygonColor = Color.FromArgb(0xB0, 0xB0, 0xB0);
        private static readonly Color ControlPointColor = Color.FromArgb(0x15, 0x65, 0xC0);

        public bool IsEnabled(RenderContext context) => true;

        public void Execute(RenderContext context)
        {
            Light light = context.Scene.Light;
            IReadOnlyList<Vector2> controlPoints = light.Path.ControlPoints;

            using Graphics graphics = Graphics.FromImage(context.Output);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Wielobok kontrolny
            var polygon = new PointF[controlPoints.Count];
            for (int i = 0; i < controlPoints.Count; i++)
                polygon[i] = ToScreen(context, controlPoints[i], light.Z);
            using (var polygonPen = new Pen(PolygonColor) { DashStyle = DashStyle.Dash })
                graphics.DrawLines(polygonPen, polygon);

            // Krzywa jako łamana z próbek P(t)
            var curve = new PointF[CurveSamples + 1];
            for (int k = 0; k <= CurveSamples; k++)
                curve[k] = ToScreen(context, light.Path.Evaluate((float)k / CurveSamples), light.Z);
            using (var curvePen = new Pen(CurveColor, 2f))
                graphics.DrawLines(curvePen, curve);

            // Punkty kontrolne (V6 pokrywa się z V0)
            using (var pointBrush = new SolidBrush(ControlPointColor))
            {
                foreach (PointF p in polygon)
                    graphics.FillRectangle(pointBrush, p.X - ControlPointSize / 2, p.Y - ControlPointSize / 2, ControlPointSize, ControlPointSize);
            }

            // Aktualne położenie światła
            PointF center = context.ToScreenPoint(light.Position);
            var bounds = new RectangleF(center.X - LightRadius, center.Y - LightRadius, 2 * LightRadius, 2 * LightRadius);
            graphics.FillEllipse(Brushes.Gold, bounds);
            graphics.DrawEllipse(Pens.Black, bounds);
        }

        private static PointF ToScreen(RenderContext context, Vector2 xy, float z) =>
            context.ToScreenPoint(new Vector3(xy.X, xy.Y, z));
    }
}
