using System.Numerics;
using Projekt2GK.Model;

namespace Projekt2GK.Rendering.Stages
{
    public class TransformStage : IRenderStage
    {
        public bool IsEnabled(RenderContext context) => true;

        public void Execute(RenderContext context)
        {
            Scene scene = context.Scene;
            Matrix4x4 rotation = scene.RotationMatrix;

            foreach (Surface surface in scene.Surfaces)
            {
                if (surface.Mesh is { } mesh && mesh.AppliedOrientationVersion != scene.OrientationVersion)
                    mesh.ApplyRotation(rotation, scene.OrientationVersion);
            }
        }
    }
}
