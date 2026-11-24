using System.Numerics;
using Projekt2GK.Utils;

namespace Projekt2GK.Geometry
{
    /// <summary>
    /// Bikubiczna powierzchnia Béziera opisana siatką 4x4 punktów kontrolnych V[i, j].
    /// Indeks i odpowiada parametrowi u, indeks j parametrowi v.
    /// </summary>
    public class BezierSurface
    {
        public const int Degree = 3;
        public const int PointsPerSide = Degree + 1;
        public const int ControlPointCount = PointsPerSide * PointsPerSide;

        private readonly Vector3[,] controlPoints;

        /// <param name="controlPoints">
        /// 16 punktów w kolejności z pliku: V00, V01, V02, V03, V10, ..., V33.
        /// </param>
        public BezierSurface(IReadOnlyList<Vector3> controlPoints)      
        {
            if (controlPoints.Count != ControlPointCount)
                throw new ArgumentException($"Powierzchnia wymaga dokładnie {ControlPointCount} punktów kontrolnych.", nameof(controlPoints));

            this.controlPoints = new Vector3[PointsPerSide, PointsPerSide];
            for (int k = 0; k < ControlPointCount; k++)
                this.controlPoints[k / PointsPerSide, k % PointsPerSide] = controlPoints[k];
        }

        public Vector3 GetControlPoint(int i, int j) => controlPoints[i, j];

        /// <summary>
        /// Wyznacza punkt powierzchni, wektory styczne i wersor normalny dla parametrów (u, v) z [0, 1].
        /// </summary>
        public SurfacePoint Evaluate(float u, float v)
        {
            // Bazy Bernsteina stopnia 3 (do P) i stopnia 2 (do pochodnych).
            float[] bu = MathUtils.BernsteinBasis(Degree, u);
            float[] bv = MathUtils.BernsteinBasis(Degree, v);
            float[] bu2 = MathUtils.BernsteinBasis(Degree - 1, u);
            float[] bv2 = MathUtils.BernsteinBasis(Degree - 1, v);

            // P(u, v) = sum_i sum_j V[i, j] * B(i, 3)(u) * B(j, 3)(v)
            Vector3 point = Vector3.Zero;
            for (int i = 0; i < PointsPerSide; i++)
                for (int j = 0; j < PointsPerSide; j++)
                    point += controlPoints[i, j] * (bu[i] * bv[j]);

            // Pu(u, v) = 3 * sum_{i<3} sum_j (V[i+1, j] - V[i, j]) * B(i, 2)(u) * B(j, 3)(v)
            Vector3 tangentU = Vector3.Zero;
            for (int i = 0; i < Degree; i++)
                for (int j = 0; j < PointsPerSide; j++)
                    tangentU += (controlPoints[i + 1, j] - controlPoints[i, j]) * (bu2[i] * bv[j]);
            tangentU *= Degree;

            // Pv(u, v) = 3 * sum_i sum_{j<3} (V[i, j+1] - V[i, j]) * B(i, 3)(u) * B(j, 2)(v)
            Vector3 tangentV = Vector3.Zero;
            for (int i = 0; i < PointsPerSide; i++)
                for (int j = 0; j < Degree; j++)
                    tangentV += (controlPoints[i, j + 1] - controlPoints[i, j]) * (bu[i] * bv2[j]);
            tangentV *= Degree;

            // N = Pu x Pv; w punktach zdegenerowanych (Pu lub Pv = 0) przyjmujemy kierunek osi z.
            Vector3 normal = GeometryUtils.SafeNormalize(Vector3.Cross(tangentU, tangentV), Vector3.UnitZ);

            return new SurfacePoint(point, tangentU, tangentV, normal);
        }
    }

    /// <summary>
    /// Wynik obliczenia powierzchni w punkcie (u, v).
    /// </summary>
    public readonly record struct SurfacePoint(Vector3 Position, Vector3 TangentU, Vector3 TangentV, Vector3 Normal);
}
