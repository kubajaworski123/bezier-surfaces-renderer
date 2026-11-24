namespace Projekt2GK.Rendering.Stages
{
    /// <summary>
    /// Czyści bufor kolorów i Z-bufor przed rysowaniem klatki.
    /// </summary>
    public class ClearStage : IRenderStage
    {
        public const int BackgroundColor = unchecked((int)0xFFFFFFFF);

        public bool IsEnabled(RenderContext context) => true;

        public void Execute(RenderContext context)
        {
            context.FrameBuffer.Clear(BackgroundColor);
            context.DepthBuffer.Clear();
        }
    }
}
