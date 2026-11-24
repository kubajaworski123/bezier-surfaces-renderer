using System.Numerics;
using Projekt2GK.Model;
using Projekt2GK.Utils;

namespace Projekt2GK.Rendering.Shading
{
    /// <summary>
    /// Sposób wyznaczenia wektora normalnego w punkcie (wzorzec Strategy):
    /// normalna powierzchni albo normalna zmodyfikowana mapą normalnych.
    /// </summary>
    public interface INormalProvider
    {
        /// <summary>
        /// Czy strategia korzysta z wektorów stycznych. Jeśli nie, wywołujący może ich nie interpolować
        /// (i przekazać wektory zerowe), co oszczędza obliczenia dla każdego piksela.
        /// </summary>
        bool NeedsTangents { get; }

        /// <param name="surfaceNormal">Wersor normalny powierzchni (po obrocie, interpolowany).</param>
        /// <param name="tangentU">Wektor styczny Pu (po obrocie, interpolowany).</param>
        /// <param name="tangentV">Wektor styczny Pv (po obrocie, interpolowany).</param>
        /// <returns>Wersor normalny używany w modelu oświetlenia.</returns>
        Vector3 GetNormal(Vector3 surfaceNormal, Vector3 tangentU, Vector3 tangentV, float u, float v);
    }

    /// <summary>Normalna wyznaczona z powierzchni, bez modyfikacji.</summary>
    public sealed class SurfaceNormalProvider : INormalProvider
    {
        public static readonly SurfaceNormalProvider Instance = new();

        public bool NeedsTangents => false;

        public Vector3 GetNormal(Vector3 surfaceNormal, Vector3 tangentU, Vector3 tangentV, float u, float v) => surfaceNormal;
    }

    /// <summary>
    /// Normalna zmodyfikowana mapą normalnych: N = M · N_tekstury, gdzie M = [Pu, Pv, N_powierzchni]
    /// (kolumny macierzy, znormalizowane). Wektor z mapy jest w przestrzeni stycznej: x wzdłuż Pu,
    /// y wzdłuż Pv, z wzdłuż normalnej powierzchni.
    /// </summary>
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
