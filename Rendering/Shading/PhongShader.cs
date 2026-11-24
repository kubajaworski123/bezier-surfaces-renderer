using System.Numerics;
using Projekt2GK.Utils;

namespace Projekt2GK.Rendering.Shading
{
    public static class PhongShader
    {
        public static readonly Vector3 ViewDirection = Vector3.UnitZ;

        public static Vector3 Shade(Vector3 position, Vector3 normal, Vector3 objectColor, in LightingParameters parameters)
        {
            if (Vector3.Dot(normal, ViewDirection) < 0f)
                normal = -normal;

            Vector3 toLight = GeometryUtils.SafeNormalize(parameters.LightPosition - position, ViewDirection);

            float cosNL = Vector3.Dot(normal, toLight);
            if (cosNL <= 0f)
                return Vector3.Zero;

            Vector3 lightTimesObject = parameters.LightColor * objectColor;
            Vector3 diffuse = parameters.Kd * cosNL * lightTimesObject;

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
