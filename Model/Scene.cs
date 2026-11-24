using System.Numerics;
using Projekt2GK.Geometry;

namespace Projekt2GK.Model
{
    public class Scene
    {
        private List<Surface> surfaces = [];

        public IReadOnlyList<Surface> Surfaces => surfaces;

        public Quaternion Orientation { get; private set; } = Quaternion.Normalize(new Quaternion(1, 1, 0, 1));

        public int OrientationVersion { get; private set; }

        public Matrix4x4 RotationMatrix => Matrix4x4.CreateFromQuaternion(Orientation);

        public Light Light { get; } = new();

        private static readonly Vector3[] DefaultColors =
        [
            new(0.55f, 0.75f, 0.95f),
            new(0.95f, 0.65f, 0.35f),
            new(0.6f, 0.9f, 0.55f),
        ];

        public void SetSurfaces(IEnumerable<BezierSurface> beziers, Texture? defaultTexture, NormalMap? defaultNormalMap)
        {
            surfaces = beziers
                .Select((b, i) => new Surface(b,
                    new Material(DefaultColors[i % DefaultColors.Length], defaultTexture, defaultNormalMap)))
                .ToList();
        }

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
