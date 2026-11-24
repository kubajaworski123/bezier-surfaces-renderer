using Projekt2GK.Geometry;

namespace Projekt2GK.Model
{
    /// <summary>
    /// Powierzchnia sceny: opis Béziera, jej aktualna siatka trójkątów i materiał.
    /// </summary>
    public class Surface
    {
        public Surface(BezierSurface bezier, Material material)
        {
            Bezier = bezier;
            Material = material;
        }

        public BezierSurface Bezier { get; }

        /// <summary>Kolor obiektu, tekstura i mapa normalnych - niezależne dla każdej powierzchni.</summary>
        public Material Material { get; }

        /// <summary>Siatka trójkątów; null, dopóki potok nie wykona triangulacji.</summary>
        public Mesh? Mesh { get; private set; }

        /// <summary>Dokładność, z jaką wyznaczono aktualną siatkę.</summary>
        public int MeshSteps { get; private set; }

        public void SetMesh(Mesh mesh, int steps)
        {
            Mesh = mesh;
            MeshSteps = steps;
        }
    }
}
