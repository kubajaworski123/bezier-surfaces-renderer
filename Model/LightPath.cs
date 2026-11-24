using System.Numerics;
using Projekt2GK.Geometry;

namespace Projekt2GK.Model
{
    /// <summary>
    /// Zamknięta krzywa Béziera 6. stopnia (7 punktów, V6 = V0), po której porusza się źródło światła.
    /// Leży na płaszczyźnie z = const; tutaj przechowywane są tylko współrzędne x, y.
    /// </summary>
    public class LightPath
    {
        public const int ControlPointCount = 7;
        private const int LastIndex = ControlPointCount - 1;

        private static readonly Vector2[] DefaultControlPoints =
        [
            new(1.3f, 0f),
            new(1.6f, 1.6f),
            new(-0.6f, 1.7f),
            new(-1.7f, 0f),
            new(-0.6f, -1.7f),
            new(1.6f, -1.6f),
            new(1.3f, 0f), // V6 = V0
        ];

        private readonly Vector2[] controlPoints = (Vector2[])DefaultControlPoints.Clone();

        public IReadOnlyList<Vector2> ControlPoints => controlPoints;

        public Vector2 Evaluate(float t) => BezierCurve.Evaluate(controlPoints, t);

        /// <summary>
        /// Przesuwa punkt kontrolny. Punkty V0 i V6 przesuwają się razem, żeby krzywa pozostała zamknięta.
        /// </summary>
        public void MoveControlPoint(int index, Vector2 position)
        {
            controlPoints[index] = position;
            if (index == 0)
                controlPoints[LastIndex] = position;
            else if (index == LastIndex)
                controlPoints[0] = position;
        }

        public void Reset() => DefaultControlPoints.CopyTo(controlPoints, 0);
    }
}
