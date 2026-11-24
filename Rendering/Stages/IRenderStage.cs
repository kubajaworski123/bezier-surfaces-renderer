namespace Projekt2GK.Rendering.Stages
{
    public interface IRenderStage
    {
        bool IsEnabled(RenderContext context);

        void Execute(RenderContext context);
    }
}
