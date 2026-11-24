using System.Numerics;
using Projekt2GK.Utils;

namespace Projekt2GK.Interaction
{
    /// <summary>
    /// Arcball (K. Shoemake): zamiana ruchu myszy na przyrost obrotu 3D.
    /// Pozycja kursora jest rzutowana na półsferę o środku w środku płótna. Przyrost obrotu
    /// to obrót przenoszący poprzedni punkt sfery na bieżący: oś = p0 x p1, kąt = acos(p0 · p1).
    /// </summary>
    public static class ArcballRotation
    {
        private const float MinAxisLength = 1e-6f;

        /// <summary>
        /// Rzutuje punkt płótna na sferę jednostkową w układzie sceny (x w prawo, y w górę, z do obserwatora).
        /// Promień sfery to połowa krótszego boku płótna. Punkty poza okręgiem trafiają na równik (z = 0).
        /// </summary>
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

        /// <summary>
        /// Przyrost obrotu przenoszący wersor <paramref name="from"/> na wersor <paramref name="to"/>.
        /// </summary>
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
