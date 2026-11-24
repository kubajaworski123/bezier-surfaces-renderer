using System.Numerics;

namespace Projekt2GK.Geometry
{
    /// <summary>
    /// Trójkąt siatki. Wierzchołki są współdzielone między sąsiednimi trójkątami,
    /// dzięki czemu każdy z nich jest obracany tylko raz.
    /// </summary>
    public readonly record struct Triangle(Vertex A, Vertex B, Vertex C);

    /// <summary>
    /// Siatka trójkątów powstała z triangulacji powierzchni.
    /// </summary>
    public class Mesh
    {
        public Mesh(IReadOnlyList<Vertex> vertices, IReadOnlyList<Triangle> triangles)
        {
            Vertices = vertices;
            Triangles = triangles;
        }

        public IReadOnlyList<Vertex> Vertices { get; }
        public IReadOnlyList<Triangle> Triangles { get; }

        /// <summary>
        /// Wersja orientacji sceny, dla której obrócono wierzchołki (-1: jeszcze nie obrócono).
        /// </summary>
        public int AppliedOrientationVersion { get; private set; } = -1;

        public void ApplyRotation(Matrix4x4 rotation, int orientationVersion)
        {
            foreach (Vertex vertex in Vertices)
                vertex.ApplyRotation(rotation);
            AppliedOrientationVersion = orientationVersion;
        }
    }
}
