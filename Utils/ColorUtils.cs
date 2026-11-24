using System.Numerics;

namespace Projekt2GK.Utils
{
    public static class ColorUtils
    {
        public static Vector3 ToVector(Color color) => new(color.R / 255f, color.G / 255f, color.B / 255f);

        public static Color ToColor(Vector3 rgb) => Color.FromArgb(ToByte(rgb.X), ToByte(rgb.Y), ToByte(rgb.Z));

        public static int ToArgb(Vector3 rgb) =>
            unchecked((int)0xFF000000) | (ToByte(rgb.X) << 16) | (ToByte(rgb.Y) << 8) | ToByte(rgb.Z);

        public static Vector3 ArgbToVector(int argb) =>
            new(((argb >> 16) & 0xFF) / 255f, ((argb >> 8) & 0xFF) / 255f, (argb & 0xFF) / 255f);

        public static Vector3 ArgbToNormal(int argb)
        {
            Vector3 normal = ArgbToVector(argb) * 2f - Vector3.One;
            return GeometryUtils.SafeNormalize(normal, Vector3.UnitZ);
        }

        private static int ToByte(float value) => (int)(MathUtils.Clamp01(value) * 255f + 0.5f);
    }
}
