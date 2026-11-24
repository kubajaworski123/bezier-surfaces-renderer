using System.Numerics;

namespace Projekt2GK.Geometry
{
    public readonly record struct Triangle(Vertex A, Vertex B, Vertex C);

    public class Mesh
    {
        public Mesh(IReadOnlyList<Vertex> vertices, IReadOnlyList<Triangle> triangles)
        {
            Vertices = vertices;
            Triangles = triangles;
        }

        public IReadOnlyList<Vertex> Vertices { get; }
        public IReadOnlyList<Triangle> Triangles { get; }

        public int AppliedOrientationVersion { get; private set; } = -1;

        public void ApplyRotation(Matrix4x4 rotation, int orientationVersion)
        {
            foreach (Vertex vertex in Vertices)
                vertex.ApplyRotation(rotation);
            AppliedOrientationVersion = orientationVersion;
        }
    }
}
