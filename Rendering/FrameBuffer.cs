using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Projekt2GK.Rendering
{
    /// <summary>
    /// Bufor kolorów rysowanej klatki: tablica pikseli w formacie 0xAARRGGBB, wiersz po wierszu.
    /// Współrzędne pikseli są ekranowe: (0, 0) to lewy górny róg.
    /// </summary>
    public class FrameBuffer
    {
        public FrameBuffer(int width, int height)
        {
            Width = width;
            Height = height;
            Pixels = new int[width * height];
        }

        public int Width { get; }
        public int Height { get; }
        public int[] Pixels { get; }

        public void Clear(int color) => Array.Fill(Pixels, color);

        /// <summary>
        /// Kopiuje całą zawartość bufora do bitmapy o tym samym rozmiarze (format 32bppPArgb).
        /// Wszystkie piksele mają alfę 255, więc wartości ARGB i PARGB są identyczne - bez przeliczania.
        /// </summary>
        public void CopyTo(Bitmap bitmap)
        {
            var rect = new Rectangle(0, 0, Width, Height);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
            try
            {
                // Dla 32 bitów na piksel stride == Width * 4, więc można skopiować wszystko naraz.
                Marshal.Copy(Pixels, 0, data.Scan0, Pixels.Length);
            }
            finally
            {
                bitmap.UnlockBits(data);
            }
        }
    }
}
