using System.Numerics;
using Projekt2GK.Geometry;
using Projekt2GK.Model;
using Projekt2GK.Rendering.Filling;
using Projekt2GK.Rendering.Shading;
using Projekt2GK.Utils;

namespace Projekt2GK.Rendering.Stages
{
    public sealed class FillStage : IRenderStage
    {
        private const float MinDoubleArea = 1e-6f;

        private readonly IPolygonFiller filler;
        private readonly SpanCallback shadeSpan;
        private readonly Vector2[] screenVertices = new Vector2[3];

        private RenderContext context = null!;
        private LightingParameters lighting;
        private IColorSource colorSource = null!;
        private INormalProvider normalProvider = SurfaceNormalProvider.Instance;
        private Triangle triangle;
        private float inverseDoubleArea;

        private Vector3 lambdaStepX;

        public FillStage(IPolygonFiller filler)
        {
            this.filler = filler;
            shadeSpan = ShadeSpan;
        }

        public bool IsEnabled(RenderContext context) => context.Settings.ShowFilledTriangles;

        public void Execute(RenderContext context)
        {
            this.context = context;
            RenderSettings settings = context.Settings;
            Light light = context.Scene.Light;
            lighting = new LightingParameters(settings.Kd, settings.Ks, settings.M, light.Color, light.Position);

            foreach (Surface surface in context.Scene.Surfaces)
            {
                if (surface.Mesh is null)
                    continue;

                Material material = surface.Material;
                colorSource = material.ColorSource;
                normalProvider = material.ActiveNormalMap is { } normalMap
                    ? new NormalMapNormalProvider(normalMap)
                    : SurfaceNormalProvider.Instance;

                foreach (Triangle t in surface.Mesh.Triangles)
                    FillTriangle(t);
            }
        }

        private void FillTriangle(Triangle t)
        {
            screenVertices[0] = context.ToScreen(t.A.RotatedPosition);
            screenVertices[1] = context.ToScreen(t.B.RotatedPosition);
            screenVertices[2] = context.ToScreen(t.C.RotatedPosition);

            float doubleArea = GeometryUtils.CrossProduct2D(screenVertices[1] - screenVertices[0], screenVertices[2] - screenVertices[0]);
            if (MathF.Abs(doubleArea) < MinDoubleArea)
                return;

            triangle = t;
            inverseDoubleArea = 1f / doubleArea;
            lambdaStepX = GeometryUtils.BarycentricStepX(screenVertices[0], screenVertices[1], screenVertices[2], inverseDoubleArea);
            filler.Fill(screenVertices, context.Width, context.Height, shadeSpan);
        }

        private void ShadeSpan(int y, int xStart, int xEnd)
        {
            int[] pixels = context.FrameBuffer.Pixels;
            DepthBuffer depth = context.DepthBuffer;
            Vertex a = triangle.A, b = triangle.B, c = triangle.C;
            Vector2 sa = screenVertices[0], sb = screenVertices[1], sc = screenVertices[2];

            Vector3 lambda = GeometryUtils.Barycentric(new Vector2(xStart + 0.5f, y + 0.5f), sa, sb, sc, inverseDoubleArea);
            Vector3 step = lambdaStepX;

            int rowStart = y * context.Width;
            bool needsTangents = normalProvider.NeedsTangents;

            for (int x = xStart; x < xEnd; x++, lambda += step)
            {
                float z = GeometryUtils.Interpolate(lambda, a.RotatedPosition.Z, b.RotatedPosition.Z, c.RotatedPosition.Z);
                int index = rowStart + x;
                if (!depth.TryUpdate(index, z))
                    continue;

                Vector3 position = GeometryUtils.Interpolate(lambda, a.RotatedPosition, b.RotatedPosition, c.RotatedPosition);
                Vector3 normal = GeometryUtils.SafeNormalize(
                    GeometryUtils.Interpolate(lambda, a.RotatedNormal, b.RotatedNormal, c.RotatedNormal), Vector3.UnitZ);
                float u = GeometryUtils.Interpolate(lambda, a.U, b.U, c.U);
                float v = GeometryUtils.Interpolate(lambda, a.V, b.V, c.V);

                Vector3 tangentU = Vector3.Zero, tangentV = Vector3.Zero;
                if (needsTangents)
                {
                    tangentU = GeometryUtils.Interpolate(lambda, a.RotatedTangentU, b.RotatedTangentU, c.RotatedTangentU);
                    tangentV = GeometryUtils.Interpolate(lambda, a.RotatedTangentV, b.RotatedTangentV, c.RotatedTangentV);
                }
                normal = normalProvider.GetNormal(normal, tangentU, tangentV, u, v);

                Vector3 objectColor = colorSource.GetColor(u, v);
                Vector3 color = PhongShader.Shade(position, normal, objectColor, lighting);
                pixels[index] = ColorUtils.ToArgb(color);
            }
        }
    }
}
