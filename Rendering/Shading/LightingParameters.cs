using System.Numerics;

namespace Projekt2GK.Rendering.Shading
{
    /// <summary>
    /// Parametry modelu oświetlenia wspólne dla całej klatki.
    /// </summary>
    /// <param name="Kd">Współczynnik składowej rozproszonej, [0, 1].</param>
    /// <param name="Ks">Współczynnik składowej zwierciadlanej, [0, 1].</param>
    /// <param name="M">Wykładnik zwierciadlany, [1, 100].</param>
    /// <param name="LightColor">Kolor światła I_L, RGB z [0, 1].</param>
    /// <param name="LightPosition">Pozycja punktowego źródła światła w układzie obserwatora.</param>
    public readonly record struct LightingParameters(
        float Kd,
        float Ks,
        int M,
        Vector3 LightColor,
        Vector3 LightPosition);
}
