using System.Numerics;

namespace Projekt2GK.Utils
{
    public static class ProjectionUtils
    {
        private static readonly float SceneRadius = MathF.Sqrt(3f);

        private const float Margin = 0.98f;

        public static float GetScale(int width, int height) => Math.Min(width, height) / 2f / SceneRadius * Margin;

        public static Vector2 ToScreen(Vector3 point, int width, int height)
        {
            float scale = GetScale(width, height);
            return new Vector2(width / 2f + point.X * scale, height / 2f - point.Y * scale);
        }

        public static Vector2 ToScene(Vector2 screenPoint, int width, int height)
        {
            float scale = GetScale(width, height);
            return new Vector2((screenPoint.X - width / 2f) / scale, (height / 2f - screenPoint.Y) / scale);
        }
    }
}
