using System.Numerics;
using Projekt2GK.Utils;

namespace Projekt2GK.Geometry
{
    /// <summary>
    /// Wierzchołek siatki trójkątów: dane przed obrotem, dane po obrocie oraz parametry (u, v).
    /// </summary>
    public class Vertex
    {
        public Vertex(SurfacePoint surfacePoint, float u, float v)
        {
            Position = surfacePoint.Position;
            TangentU = surfacePoint.TangentU;
            TangentV = surfacePoint.TangentV;
            Normal = surfacePoint.Normal;
            U = u;
            V = v;
            ApplyRotation(Matrix4x4.Identity);
        }

        // Przed obrotem
        public Vector3 Position { get; }
        public Vector3 TangentU { get; }
        public Vector3 TangentV { get; }
        public Vector3 Normal { get; }

        // Po obrocie
        public Vector3 RotatedPosition { get; private set; }
        public Vector3 RotatedTangentU { get; private set; }
        public Vector3 RotatedTangentV { get; private set; }
        public Vector3 RotatedNormal { get; private set; }

        // Parametry powierzchni
        public float U { get; }
        public float V { get; }

        /// <summary>
        /// Obraca wierzchołek wokół środka sceny (0, 0, 0).
        /// Punkt jest przekształcany jako punkt, a wektory styczne i normalny jako wektory kierunkowe.
        /// </summary>
        public void ApplyRotation(Matrix4x4 rotation)
        {
            RotatedPosition = Vector3.Transform(Position, rotation);
            RotatedTangentU = Vector3.TransformNormal(TangentU, rotation);
            RotatedTangentV = Vector3.TransformNormal(TangentV, rotation);
            RotatedNormal = GeometryUtils.SafeNormalize(Vector3.TransformNormal(Normal, rotation), Vector3.UnitZ);
        }
    }
}
