using Projekt2GK.Geometry;
using Projekt2GK.Model;

namespace Projekt2GK.Rendering.Stages
{
    public class MeshOverlayStage : IRenderStage
    {
        private static readonly Color LineColor = Color.FromArgb(0x2E, 0x7D, 0x32);

        public bool IsEnabled(RenderContext context) => context.Settings.ShowTriangleMesh;

        public void Execute(RenderContext context)
        {
            using Graphics graphics = Graphics.FromImage(context.Output);
            using var pen = new Pen(LineColor);
            var corners = new PointF[3];

            foreach (Surface surface in context.Scene.Surfaces)
            {
                if (surface.Mesh is null)
                    continue;

                foreach (Triangle triangle in surface.Mesh.Triangles)
                {
                    corners[0] = context.ToScreenPoint(triangle.A.RotatedPosition);
                    corners[1] = context.ToScreenPoint(triangle.B.RotatedPosition);
                    corners[2] = context.ToScreenPoint(triangle.C.RotatedPosition);
                    graphics.DrawPolygon(pen, corners);
                }
            }
        }
    }
}
