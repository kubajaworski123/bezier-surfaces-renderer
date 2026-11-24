using Projekt2GK.Model;
using Projekt2GK.Rendering.Filling;
using Projekt2GK.Rendering.Stages;

namespace Projekt2GK.Rendering
{
    public sealed class SceneRenderer : IDisposable
    {
        private readonly RenderContext context;
        private readonly RenderPipeline pipeline;

        public SceneRenderer(Scene scene, RenderSettings settings, int width, int height)
        {
            context = new RenderContext(scene, settings, width, height);
            pipeline = new RenderPipeline(
            [
                new TessellationStage(),
                new TransformStage(),
                new ClearStage(),
                new FillStage(new EdgeBucketFiller()),
                new PresentStage(),
                new MeshOverlayStage(),
                new ControlPolygonOverlayStage(),
                new LightOverlayStage(),
            ]);
        }

        public Bitmap Output => context.Output;

        public void Render() => pipeline.Run(context);

        public void Dispose() => context.Dispose();
    }
}
