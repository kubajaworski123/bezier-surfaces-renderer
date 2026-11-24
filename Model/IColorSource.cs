using System.Numerics;

namespace Projekt2GK.Model
{
    public interface IColorSource
    {
        Vector3 GetColor(float u, float v);
    }

    public sealed class SolidColorSource : IColorSource
    {
        public SolidColorSource(Vector3 color)
        {
            Color = color;
        }

        public Vector3 Color { get; }

        public Vector3 GetColor(float u, float v) => Color;
    }

    public sealed class TextureColorSource : IColorSource
    {
        public TextureColorSource(Texture texture)
        {
            Texture = texture;
        }

        public Texture Texture { get; }

        public Vector3 GetColor(float u, float v) => Texture.SampleColor(u, v);
    }
}
