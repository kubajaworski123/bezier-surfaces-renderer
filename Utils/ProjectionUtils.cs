using System.Numerics;

namespace Projekt2GK.Utils
{
    /// <summary>
    /// Rzut prostokątny sceny na obszar rysowania.
    /// Układ sceny: środek (0, 0, 0) w środku obszaru, oś x w prawo, oś y w górę, oś z do obserwatora.
    /// Układ ekranu: (0, 0) w lewym górnym rogu, oś y w dół.
    /// </summary>
    public static class ProjectionUtils
    {
        /// <summary>
        /// Połowa przekątnej sześcianu [-1, 1]^3. Kula o tym promieniu zawiera scenę przy każdym obrocie.
        /// </summary>
        private static readonly float SceneRadius = MathF.Sqrt(3f);

        private const float Margin = 0.98f;

        /// <summary>Liczba pikseli na jednostkę sceny.</summary>
        public static float GetScale(int width, int height) => Math.Min(width, height) / 2f / SceneRadius * Margin;

        /// <summary>Rzut prostokątny punktu sceny: pomija z i przelicza x, y na piksele.</summary>
        public static Vector2 ToScreen(Vector3 point, int width, int height)
        {
            float scale = GetScale(width, height);
            return new Vector2(width / 2f + point.X * scale, height / 2f - point.Y * scale);
        }

        /// <summary>Odwrotność <see cref="ToScreen"/> dla współrzędnych x, y.</summary>
        public static Vector2 ToScene(Vector2 screenPoint, int width, int height)
        {
            float scale = GetScale(width, height);
            return new Vector2((screenPoint.X - width / 2f) / scale, (height / 2f - screenPoint.Y) / scale);
        }
    }
}
