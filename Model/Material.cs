using System.Numerics;

namespace Projekt2GK.Model
{
    public class Material
    {
        public Material(Vector3 solidColor, Texture? texture, NormalMap? normalMap)
        {
            SolidColor = new SolidColorSource(solidColor);
            TextureSource = texture is null ? null : new TextureColorSource(texture);
            NormalMap = normalMap;
        }

        public SolidColorSource SolidColor { get; set; }
        public TextureColorSource? TextureSource { get; set; }

        public bool UseTexture { get; set; }

        public NormalMap? NormalMap { get; set; }

        public bool UseNormalMap { get; set; }

        public IColorSource ColorSource =>
            UseTexture && TextureSource is not null ? TextureSource : SolidColor;

        public NormalMap? ActiveNormalMap => UseNormalMap ? NormalMap : null;
    }
}
