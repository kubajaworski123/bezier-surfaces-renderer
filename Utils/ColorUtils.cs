using System.Numerics;

namespace Projekt2GK.Utils
{
    /// <summary>
    /// Zamiana kolorów między System.Drawing / formatem ARGB a wektorami RGB z przedziału [0, 1],
    /// na których wykonywane są obliczenia oświetlenia.
    /// </summary>
    public static class ColorUtils
    {
        public static Vector3 ToVector(Color color) => new(color.R / 255f, color.G / 255f, color.B / 255f);

        public static Color ToColor(Vector3 rgb) => Color.FromArgb(ToByte(rgb.X), ToByte(rgb.Y), ToByte(rgb.Z));

        /// <summary>
        /// Kolor RGB z [0, 1] na piksel 0xFFRRGGBB. Wartości są przeliczane na 0–255 i obcinane.
        /// </summary>
        public static int ToArgb(Vector3 rgb) =>
            unchecked((int)0xFF000000) | (ToByte(rgb.X) << 16) | (ToByte(rgb.Y) << 8) | ToByte(rgb.Z);

        /// <summary>Piksel 0xAARRGGBB na kolor RGB z [0, 1].</summary>
        public static Vector3 ArgbToVector(int argb) =>
            new(((argb >> 16) & 0xFF) / 255f, ((argb >> 8) & 0xFF) / 255f, (argb & 0xFF) / 255f);

        /// <summary>
        /// Piksel mapy normalnych na wersor: każda składowa c z 0–255 przechodzi na c / 255 · 2 − 1 z [-1, 1].
        /// </summary>
        public static Vector3 ArgbToNormal(int argb)
        {
            Vector3 normal = ArgbToVector(argb) * 2f - Vector3.One;
            return GeometryUtils.SafeNormalize(normal, Vector3.UnitZ);
        }

        private static int ToByte(float value) => (int)(MathUtils.Clamp01(value) * 255f + 0.5f);
    }
}
