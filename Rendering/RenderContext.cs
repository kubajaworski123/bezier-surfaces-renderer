using System.Drawing.Imaging;
using System.Numerics;
using Projekt2GK.Model;
using Projekt2GK.Utils;

namespace Projekt2GK.Rendering
{
    /// <summary>
    /// Dane współdzielone przez etapy potoku renderowania jednej klatki.
    /// </summary>
    public sealed class RenderContext : IDisposable
    {
        public RenderContext(Scene scene, RenderSettings settings, int width, int height)
        {
            Scene = scene;
            Settings = settings;
            FrameBuffer = new FrameBuffer(width, height);
            DepthBuffer = new DepthBuffer(width, height);
            Output = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
        }

        public Scene Scene { get; }
        public RenderSettings Settings { get; }
        public FrameBuffer FrameBuffer { get; }
        public DepthBuffer DepthBuffer { get; }

        /// <summary>Bitmapa z gotową klatką, wyświetlana na płótnie.</summary>
        public Bitmap Output { get; }

        public int Width => FrameBuffer.Width;
        public int Height => FrameBuffer.Height;

        public Vector2 ToScreen(Vector3 scenePoint) => ProjectionUtils.ToScreen(scenePoint, Width, Height);

        /// <summary>Jak <see cref="ToScreen"/>, ale w typie używanym przez System.Drawing.</summary>
        public PointF ToScreenPoint(Vector3 scenePoint)
        {
            Vector2 screen = ToScreen(scenePoint);
            return new PointF(screen.X, screen.Y);
        }

        public void Dispose() => Output.Dispose();
    }
}
