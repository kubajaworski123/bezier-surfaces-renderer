using Projekt2GK.Geometry;
using Projekt2GK.Model;

namespace Projekt2GK.Rendering.Stages
{
    /// <summary>
    /// Triangulacja powierzchni. Siatka jest liczona od nowa tylko wtedy,
    /// gdy jej brak lub zmieniła się dokładność triangulacji.
    /// </summary>
    public class TessellationStage : IRenderStage
    {
        public bool IsEnabled(RenderContext context) => true;

        public void Execute(RenderContext context)
        {
            int steps = context.Settings.TriangulationSteps;
            foreach (Surface surface in context.Scene.Surfaces)
            {
                if (surface.Mesh is null || surface.MeshSteps != steps)
                    surface.SetMesh(Tessellator.Tessellate(surface.Bezier, steps), steps);
            }
        }
    }
}
