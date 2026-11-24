using System.Numerics;

namespace Projekt2GK.Rendering.Shading
{
    public readonly record struct LightingParameters(
        float Kd,
        float Ks,
        int M,
        Vector3 LightColor,
        Vector3 LightPosition);
}
