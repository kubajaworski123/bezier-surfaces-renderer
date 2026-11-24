using System.Numerics;

namespace Projekt2GK.Utils
{
    public static class GeometryUtils
    {
        private const float Epsilon = 1e-12f;

        public static Vector3 SafeNormalize(Vector3 v, Vector3 fallback)
        {
            float lengthSquared = v.LengthSquared();
            return lengthSquared > Epsilon ? v / MathF.Sqrt(lengthSquared) : fallback;
        }

        public static float CrossProduct2D(Vector2 u, Vector2 v) => u.X * v.Y - u.Y * v.X;

        public static Vector3 Barycentric(Vector2 p, Vector2 a, Vector2 b, Vector2 c, float inverseDoubleArea)
        {
            float lambdaB = CrossProduct2D(p - a, c - a) * inverseDoubleArea;
            float lambdaC = CrossProduct2D(b - a, p - a) * inverseDoubleArea;
            return new Vector3(1f - lambdaB - lambdaC, lambdaB, lambdaC);
        }

        public static Vector3 BarycentricStepX(Vector2 a, Vector2 b, Vector2 c, float inverseDoubleArea)
        {
            float stepB = (c.Y - a.Y) * inverseDoubleArea;
            float stepC = (a.Y - b.Y) * inverseDoubleArea;
            return new Vector3(-stepB - stepC, stepB, stepC);
        }

        public static Vector3 Interpolate(Vector3 lambda, Vector3 a, Vector3 b, Vector3 c) =>
            lambda.X * a + lambda.Y * b + lambda.Z * c;

        public static float Interpolate(Vector3 lambda, float a, float b, float c) =>
            lambda.X * a + lambda.Y * b + lambda.Z * c;

        public static Vector3 MultiplyByColumns(Vector3 c0, Vector3 c1, Vector3 c2, Vector3 v) =>
            c0 * v.X + c1 * v.Y + c2 * v.Z;

        public static Vector3 Reflect(Vector3 normal, Vector3 toLight) =>
            2f * Vector3.Dot(normal, toLight) * normal - toLight;
    }
}
