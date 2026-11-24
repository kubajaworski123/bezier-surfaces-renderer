using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.InteropServices;
using Projekt2GK.Utils;

namespace Projekt2GK.Model
{
    /// <summary>
    /// Obraz wczytany do tablicy pikseli (bez GetPixel w pętli renderowania).
    /// Obraz jest przeskalowany do kwadratu parametrów [0, 1] x [0, 1]:
    /// u odpowiada osi x obrazu (od lewej), v - osi y (od dołu).
    /// </summary>
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

        /// <exception cref="ArgumentException">Plik nie jest obsługiwanym obrazem.</exception>
        public static Texture Load(string path)
        {
            using var bitmap = new Bitmap(path);
            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            try
            {
                var pixels = new int[bitmap.Width * bitmap.Height];
                // Dla 32 bitów na piksel stride == Width * 4, więc można skopiować wszystko naraz.
                Marshal.Copy(data.Scan0, pixels, 0, pixels.Length);
                return new Texture(bitmap.Width, bitmap.Height, pixels);
            }
            finally
            {
                bitmap.UnlockBits(data);
            }
        }

        public int PixelCount => pixels.Length;

        /// <summary>
        /// Indeks piksela (najbliższego) odpowiadającego parametrom (u, v).
        /// v rośnie w górę obrazu (wiersze obrazu rosną w dół, stąd 1 - v), dzięki czemu obraz jest
        /// widoczny prosto, a oś y obrazu ma ten sam kierunek co Pv - ważne dla mapy normalnych.
        /// </summary>
        public int PixelIndex(float u, float v)
        {
            int x = Math.Min((int)(MathUtils.Clamp01(u) * Width), Width - 1);
            int y = Math.Min((int)(MathUtils.Clamp01(1f - v) * Height), Height - 1);
            return y * Width + x;
        }

        /// <summary>Piksel 0xAARRGGBB o danym indeksie.</summary>
        public int GetPixel(int index) => pixels[index];

        /// <summary>Kolor piksela 0xAARRGGBB odpowiadającego parametrom (u, v).</summary>
        public int SampleArgb(float u, float v) => pixels[PixelIndex(u, v)];

        /// <summary>Kolor RGB z [0, 1] odpowiadający parametrom (u, v).</summary>
        public Vector3 SampleColor(float u, float v) => ColorUtils.ArgbToVector(SampleArgb(u, v));
    }
}
