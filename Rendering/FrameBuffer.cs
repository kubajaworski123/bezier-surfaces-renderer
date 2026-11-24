using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Projekt2GK.Rendering
{
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

        public void CopyTo(Bitmap bitmap)
        {
            var rect = new Rectangle(0, 0, Width, Height);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
            try
            {
                Marshal.Copy(Pixels, 0, data.Scan0, Pixels.Length);
            }
            finally
            {
                bitmap.UnlockBits(data);
            }
        }
    }
}
