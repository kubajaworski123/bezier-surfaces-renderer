using System.Numerics;
using Projekt2GK.Utils;

namespace Projekt2GK.Rendering.Shading
{
    /// <summary>
    /// Model oświetlenia z polecenia: składowa rozproszona (Lambert) + składowa zwierciadlana.
    /// I = kd · IL · IO · cos(N, L) + ks · IL · IO · cos^m(V, R),
    /// liczone niezależnie dla R, G i B, na wartościach z [0, 1].
    /// </summary>
    public static class PhongShader
    {
        /// <summary>Kierunek do obserwatora (rzut prostokątny wzdłuż osi z).</summary>
        public static readonly Vector3 ViewDirection = Vector3.UnitZ;

        /// <param name="position">Punkt powierzchni (po obrocie).</param>
        /// <param name="normal">Wersor normalny w tym punkcie (po obrocie).</param>
        /// <param name="objectColor">Kolor obiektu I_O w tym punkcie.</param>
        /// <returns>Kolor RGB; składowe mogą przekraczać 1 - obcięcie następuje przy zamianie na piksel.</returns>
        public static Vector3 Shade(Vector3 position, Vector3 normal, Vector3 objectColor, in LightingParameters parameters)
        {
            // Dwie strony powierzchni: oświetlamy stronę zwróconą do obserwatora.
            if (Vector3.Dot(normal, ViewDirection) < 0f)
                normal = -normal;

            Vector3 toLight = GeometryUtils.SafeNormalize(parameters.LightPosition - position, ViewDirection);

            // Światło po drugiej stronie powierzchni nie oświetla jej (cosinus ujemny -> 0).
            float cosNL = Vector3.Dot(normal, toLight);
            if (cosNL <= 0f)
                return Vector3.Zero;

            Vector3 lightTimesObject = parameters.LightColor * objectColor;
            Vector3 diffuse = parameters.Kd * cosNL * lightTimesObject;

            // Składowa zwierciadlana; pomijana, gdy i tak byłaby zerem.
            if (parameters.Ks <= 0f)
                return diffuse;

            Vector3 reflected = GeometryUtils.Reflect(normal, toLight);
            float cosVR = Vector3.Dot(ViewDirection, reflected);
            if (cosVR <= 0f)
                return diffuse;

            return diffuse + parameters.Ks * MathUtils.IntPow(cosVR, parameters.M) * lightTimesObject;
        }
    }
}
