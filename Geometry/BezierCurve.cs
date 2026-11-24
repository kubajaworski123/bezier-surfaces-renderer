using System.Numerics;
using Projekt2GK.Utils;

namespace Projekt2GK.Geometry
{
    public static class BezierCurve
    {
        public static Vector2 Evaluate(IReadOnlyList<Vector2> controlPoints, float t)
        {
            int degree = controlPoints.Count - 1;
            Vector2 point = Vector2.Zero;
            for (int i = 0; i <= degree; i++)
                point += controlPoints[i] * MathUtils.Bernstein(degree, i, t);
            return point;
        }
    }
}
