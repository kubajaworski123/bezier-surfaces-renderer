using System.Numerics;
using Projekt2GK.Utils;

namespace Projekt2GK.Model
{
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

        public Vector3 Sample(float u, float v) => normals[texture.PixelIndex(u, v)];
    }
}
