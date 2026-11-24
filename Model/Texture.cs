using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.InteropServices;
using Projekt2GK.Utils;

namespace Projekt2GK.Model
{
    public class Texture
    {
        private readonly int[] pixels;

        private Texture(int width, int height, int[] pixels)
        {
            Width = width;
            Height = height;
            this.pixels = pixels;
        }

        public int Width { get; }
        public int Height { get; }

        public static Texture Load(string path)
        {
            using var bitmap = new Bitmap(path);
            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            try
            {
                var pixels = new int[bitmap.Width * bitmap.Height];
                Marshal.Copy(data.Scan0, pixels, 0, pixels.Length);
                return new Texture(bitmap.Width, bitmap.Height, pixels);
            }
            finally
            {
                bitmap.UnlockBits(data);
            }
        }

        public int PixelCount => pixels.Length;

        public int PixelIndex(float u, float v)
        {
            int x = Math.Min((int)(MathUtils.Clamp01(u) * Width), Width - 1);
            int y = Math.Min((int)(MathUtils.Clamp01(1f - v) * Height), Height - 1);
            return y * Width + x;
        }

        public int GetPixel(int index) => pixels[index];

        public int SampleArgb(float u, float v) => pixels[PixelIndex(u, v)];

        public Vector3 SampleColor(float u, float v) => ColorUtils.ArgbToVector(SampleArgb(u, v));
    }
}
