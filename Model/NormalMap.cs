using System.Numerics;
using Projekt2GK.Utils;

namespace Projekt2GK.Model
{
    /// <summary>
    /// Mapa wektorów normalnych: każdy piksel koduje wersor N_tekstury w przestrzeni stycznej
    /// (R, G, B z 0–255 odpowiadają x, y, z z [-1, 1]; RGB(127, 127, 255) to N = [0, 0, 1]).
    /// Wektory są wyznaczane raz, przy wczytaniu.
    /// </summary>
    public class NormalMap
    {
        private readonly Texture texture;
        private readonly Vector3[] normals;

        private NormalMap(Texture texture)
        {
            this.texture = texture;
            normals = new Vector3[texture.PixelCount];
            for (int i = 0; i < normals.Length; i++)
                normals[i] = ColorUtils.ArgbToNormal(texture.GetPixel(i));
        }

        public static NormalMap Load(string path) => new(Texture.Load(path));

        /// <summary>Wersor N_tekstury dla parametrów (u, v) (najbliższy piksel, jak w teksturze).</summary>
        public Vector3 Sample(float u, float v) => normals[texture.PixelIndex(u, v)];
    }
}
