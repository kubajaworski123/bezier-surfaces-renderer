using Projekt2GK.Geometry;

namespace Projekt2GK.Model
{
    public class Surface
    {
        public Surface(BezierSurface bezier, Material material)
        {
            Bezier = bezier;
            Material = material;
        }

        public BezierSurface Bezier { get; }

        public Material Material { get; }

        public Mesh? Mesh { get; private set; }

        public int MeshSteps { get; private set; }

        public void SetMesh(Mesh mesh, int steps)
        {
            Mesh = mesh;
            MeshSteps = steps;
        }
    }
}
