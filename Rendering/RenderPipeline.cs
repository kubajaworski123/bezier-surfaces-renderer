using Projekt2GK.Rendering.Stages;

namespace Projekt2GK.Rendering
{
    /// <summary>
    /// Potok renderowania: wykonuje po kolei włączone etapy.
    /// </summary>
    public class RenderPipeline
    {
        private readonly IReadOnlyList<IRenderStage> stages;

        public RenderPipeline(IEnumerable<IRenderStage> stages)
        {
            this.stages = stages.ToList();
        }

        public void Run(RenderContext context)
        {
            foreach (IRenderStage stage in stages)
            {
                if (stage.IsEnabled(context))
                    stage.Execute(context);
            }
        }
    }
}
