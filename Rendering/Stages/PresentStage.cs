namespace Projekt2GK.Rendering.Stages
{
    /// <summary>
    /// Kopiuje bufor kolorów do bitmapy wyświetlanej na płótnie. Kolejne etapy (nakładki) rysują już na bitmapie.
    /// </summary>
    public class PresentStage : IRenderStage
    {
        public bool IsEnabled(RenderContext context) => true;

        public void Execute(RenderContext context) => context.FrameBuffer.CopyTo(context.Output);
    }
}
