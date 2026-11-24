using System.Numerics;

namespace Projekt2GK.Utils
{
    /// <summary>
    /// Operacje na wektorach używane w wielu miejscach projektu.
    /// Podstawowe operacje (Dot, Cross, Normalize) zapewnia System.Numerics.Vector3.
    /// </summary>
    public static class GeometryUtils
    {
        private const float Epsilon = 1e-12f;

        /// <summary>
        /// Normalizuje wektor; dla wektora (prawie) zerowego zwraca <paramref name="fallback"/>,
        /// zamiast wartości NaN, które dałby Vector3.Normalize.
        /// </summary>
        public static Vector3 SafeNormalize(Vector3 v, Vector3 fallback)
        {
            float lengthSquared = v.LengthSquared();
            return lengthSquared > Epsilon ? v / MathF.Sqrt(lengthSquared) : fallback;
        }

        /// <summary>
        /// Iloczyn wektorowy 2D (składowa z iloczynu wektorowego 3D). Równy podwojonemu polu
        /// trójkąta rozpiętego na u i v, ze znakiem zależnym od kierunku obiegu.
        /// </summary>
        public static float CrossProduct2D(Vector2 u, Vector2 v) => u.X * v.Y - u.Y * v.X;

        /// <summary>
        /// Współrzędne barycentryczne (λa, λb, λc) punktu p względem trójkąta abc, liczone ze stosunku pól.
        /// <paramref name="inverseDoubleArea"/> = 1 / CrossProduct2D(b - a, c - a), liczone raz na trójkąt.
        /// </summary>
        public static Vector3 Barycentric(Vector2 p, Vector2 a, Vector2 b, Vector2 c, float inverseDoubleArea)
        {
            float lambdaB = CrossProduct2D(p - a, c - a) * inverseDoubleArea;
            float lambdaC = CrossProduct2D(b - a, p - a) * inverseDoubleArea;
            return new Vector3(1f - lambdaB - lambdaC, lambdaB, lambdaC);
        }

        /// <summary>
        /// Przyrost współrzędnych barycentrycznych przy przesunięciu punktu o 1 w osi x (pochodna ∂λ/∂x).
        /// λb i λc są liniowe względem p.x (wzory z <see cref="Barycentric"/>), a λa = 1 - λb - λc,
        /// więc przyrost jest stały w całym trójkącie.
        /// </summary>
        public static Vector3 BarycentricStepX(Vector2 a, Vector2 b, Vector2 c, float inverseDoubleArea)
        {
            float stepB = (c.Y - a.Y) * inverseDoubleArea;
            float stepC = (a.Y - b.Y) * inverseDoubleArea;
            return new Vector3(-stepB - stepC, stepB, stepC);
        }

        /// <summary>Interpolacja barycentryczna wektora: λa·a + λb·b + λc·c.</summary>
        public static Vector3 Interpolate(Vector3 lambda, Vector3 a, Vector3 b, Vector3 c) =>
            lambda.X * a + lambda.Y * b + lambda.Z * c;

        /// <summary>Interpolacja barycentryczna wartości skalarnej.</summary>
        public static float Interpolate(Vector3 lambda, float a, float b, float c) =>
            lambda.X * a + lambda.Y * b + lambda.Z * c;

        /// <summary>
        /// Iloczyn macierzy 3x3 o kolumnach c0, c1, c2 i wektora v: M · v = c0·v.x + c1·v.y + c2·v.z.
        /// </summary>
        public static Vector3 MultiplyByColumns(Vector3 c0, Vector3 c1, Vector3 c2, Vector3 v) =>
            c0 * v.X + c1 * v.Y + c2 * v.Z;

        /// <summary>
        /// Wektor odbicia R = 2⟨N, L⟩N − L dla wersorów N (normalny) i L (do światła).
        /// </summary>
        public static Vector3 Reflect(Vector3 normal, Vector3 toLight) =>
            2f * Vector3.Dot(normal, toLight) * normal - toLight;
    }
}
