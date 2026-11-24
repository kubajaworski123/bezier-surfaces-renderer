using System.Numerics;

namespace Projekt2GK.Model
{
    /// <summary>
    /// Źródło koloru obiektu I_O (wzorzec Strategy): jednolity kolor albo tekstura.
    /// </summary>
    public interface IColorSource
    {
        /// <summary>Kolor RGB z [0, 1] w punkcie powierzchni o parametrach (u, v).</summary>
        Vector3 GetColor(float u, float v);
    }

    /// <summary>Jednolity kolor całej powierzchni.</summary>
    public sealed class SolidColorSource : IColorSource
    {
        public SolidColorSource(Vector3 color)
        {
            Color = color;
        }

        public Vector3 Color { get; }

        public Vector3 GetColor(float u, float v) => Color;
    }

    /// <summary>Kolor odczytany z tekstury rozpiętej na całej powierzchni (parametry u, v z [0, 1]).</summary>
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
