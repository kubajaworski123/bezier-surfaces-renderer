using System.Numerics;
using Projekt2GK.Geometry;

namespace Projekt2GK.Model
{
    /// <summary>
    /// Stan sceny: powierzchnie i orientacja (obrót wokół środka sceny).
    /// </summary>
    public class Scene
    {
        private List<Surface> surfaces = [];

        public IReadOnlyList<Surface> Surfaces => surfaces;

        /// <summary>Orientacja sceny jako kwaternion jednostkowy.</summary>
        public Quaternion Orientation { get; private set; } = Quaternion.Normalize(new Quaternion(1, 1, 0, 1));

        /// <summary>Zwiększana przy każdej zmianie orientacji; pozwala etapom potoku pominąć zbędne obliczenia.</summary>
        public int OrientationVersion { get; private set; }

        /// <summary>Macierz obrotu odpowiadająca orientacji (konwencja System.Numerics: v' = v * M).</summary>
        public Matrix4x4 RotationMatrix => Matrix4x4.CreateFromQuaternion(Orientation);

        public Light Light { get; } = new();

        /// <summary>Domyślne kolory kolejnych powierzchni, żeby od razu było je widać jako osobne obiekty.</summary>
        private static readonly Vector3[] DefaultColors =
        [
            new(0.55f, 0.75f, 0.95f),
            new(0.95f, 0.65f, 0.35f),
            new(0.6f, 0.9f, 0.55f),
        ];

        /// <summary>
        /// Zastępuje powierzchnie sceny. Każda dostaje własny materiał z domyślnym kolorem,
        /// domyślną teksturą i domyślną mapą normalnych (jeśli zostały wczytane).
        /// </summary>
        public void SetSurfaces(IEnumerable<BezierSurface> beziers, Texture? defaultTexture, NormalMap? defaultNormalMap)
        {
            surfaces = beziers
                .Select((b, i) => new Surface(b,
                    new Material(DefaultColors[i % DefaultColors.Length], defaultTexture, defaultNormalMap)))
                .ToList();
        }

        /// <summary>
        /// Składa przyrost obrotu z aktualną orientacją: R_nowa = R_przyrost · R_aktualna
        /// (najpierw dotychczasowa orientacja, potem przyrost).
        /// Normalizacja zapobiega kumulowaniu się błędów numerycznych.
        /// </summary>
        public void Rotate(Quaternion delta)
        {
            Orientation = Quaternion.Normalize(Quaternion.Concatenate(Orientation, delta));
            OrientationVersion++;
        }

        public void ResetOrientation()
        {
            Orientation = Quaternion.Identity;
            OrientationVersion++;
        }
    }
}
