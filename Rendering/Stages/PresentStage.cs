namespace Projekt2GK.Rendering.Stages
{
    public class PresentStage : IRenderStage
    {
        public bool IsEnabled(RenderContext context) => true;

        public void Execute(RenderContext context) => context.FrameBuffer.CopyTo(context.Output);
    }
}
