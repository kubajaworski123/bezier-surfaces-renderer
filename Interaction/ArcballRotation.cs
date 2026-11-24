using System.Numerics;
using Projekt2GK.Utils;

namespace Projekt2GK.Interaction
{
    public static class ArcballRotation
    {
        private const float MinAxisLength = 1e-6f;

        public static Vector3 MapToSphere(Point screenPoint, Size canvasSize)
        {
            float radius = Math.Min(canvasSize.Width, canvasSize.Height) / 2f;
            float x = (screenPoint.X - canvasSize.Width / 2f) / radius;
            float y = (canvasSize.Height / 2f - screenPoint.Y) / radius;

            float lengthSquared = x * x + y * y;
            if (lengthSquared <= 1f)
                return new Vector3(x, y, MathF.Sqrt(1f - lengthSquared));

            float length = MathF.Sqrt(lengthSquared);
            return new Vector3(x / length, y / length, 0f);
        }

        public static Quaternion ComputeDelta(Vector3 from, Vector3 to)
        {
            Vector3 axis = Vector3.Cross(from, to);
            if (axis.Length() < MinAxisLength)
                return Quaternion.Identity;

            float angle = MathF.Acos(Math.Clamp(Vector3.Dot(from, to), -1f, 1f));
            return Quaternion.CreateFromAxisAngle(GeometryUtils.SafeNormalize(axis, Vector3.UnitZ), angle);
        }
    }
}
