using System.Numerics;
using Projekt2GK.Model;
using Projekt2GK.Utils;

namespace Projekt2GK.Rendering.Shading
{
    public interface INormalProvider
    {
        bool NeedsTangents { get; }

        Vector3 GetNormal(Vector3 surfaceNormal, Vector3 tangentU, Vector3 tangentV, float u, float v);
    }

    public sealed class SurfaceNormalProvider : INormalProvider
    {
        public static readonly SurfaceNormalProvider Instance = new();

        public bool NeedsTangents => false;

        public Vector3 GetNormal(Vector3 surfaceNormal, Vector3 tangentU, Vector3 tangentV, float u, float v) => surfaceNormal;
    }

    public sealed class NormalMapNormalProvider : INormalProvider
    {
        private readonly NormalMap normalMap;

        public bool NeedsTangents => true;

        public NormalMapNormalProvider(NormalMap normalMap)
        {
            this.normalMap = normalMap;
        }

        public Vector3 GetNormal(Vector3 surfaceNormal, Vector3 tangentU, Vector3 tangentV, float u, float v)
        {
            Vector3 textureNormal = normalMap.Sample(u, v);
            Vector3 pu = GeometryUtils.SafeNormalize(tangentU, Vector3.UnitX);
            Vector3 pv = GeometryUtils.SafeNormalize(tangentV, Vector3.UnitY);
            Vector3 modified = GeometryUtils.MultiplyByColumns(pu, pv, surfaceNormal, textureNormal);
            return GeometryUtils.SafeNormalize(modified, surfaceNormal);
        }
    }
}
