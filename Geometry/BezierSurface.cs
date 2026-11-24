using System.Numerics;
using Projekt2GK.Utils;

namespace Projekt2GK.Geometry
{
    public class BezierSurface
    {
        public const int Degree = 3;
        public const int PointsPerSide = Degree + 1;
        public const int ControlPointCount = PointsPerSide * PointsPerSide;

        private readonly Vector3[,] controlPoints;

        public BezierSurface(IReadOnlyList<Vector3> controlPoints)      
        {
            if (controlPoints.Count != ControlPointCount)
                throw new ArgumentException($"Powierzchnia wymaga dokładnie {ControlPointCount} punktów kontrolnych.", nameof(controlPoints));

            this.controlPoints = new Vector3[PointsPerSide, PointsPerSide];
            for (int k = 0; k < ControlPointCount; k++)
                this.controlPoints[k / PointsPerSide, k % PointsPerSide] = controlPoints[k];
        }

        public Vector3 GetControlPoint(int i, int j) => controlPoints[i, j];

        public SurfacePoint Evaluate(float u, float v)
        {
            float[] bu = MathUtils.BernsteinBasis(Degree, u);
            float[] bv = MathUtils.BernsteinBasis(Degree, v);
            float[] bu2 = MathUtils.BernsteinBasis(Degree - 1, u);
            float[] bv2 = MathUtils.BernsteinBasis(Degree - 1, v);

            Vector3 point = Vector3.Zero;
            for (int i = 0; i < PointsPerSide; i++)
                for (int j = 0; j < PointsPerSide; j++)
                    point += controlPoints[i, j] * (bu[i] * bv[j]);

            Vector3 tangentU = Vector3.Zero;
            for (int i = 0; i < Degree; i++)
                for (int j = 0; j < PointsPerSide; j++)
                    tangentU += (controlPoints[i + 1, j] - controlPoints[i, j]) * (bu2[i] * bv[j]);
            tangentU *= Degree;

            Vector3 tangentV = Vector3.Zero;
            for (int i = 0; i < PointsPerSide; i++)
                for (int j = 0; j < Degree; j++)
                    tangentV += (controlPoints[i, j + 1] - controlPoints[i, j]) * (bu[i] * bv2[j]);
            tangentV *= Degree;

            Vector3 normal = GeometryUtils.SafeNormalize(Vector3.Cross(tangentU, tangentV), Vector3.UnitZ);

            return new SurfacePoint(point, tangentU, tangentV, normal);
        }
    }

    public readonly record struct SurfacePoint(Vector3 Position, Vector3 TangentU, Vector3 TangentV, Vector3 Normal);
}
