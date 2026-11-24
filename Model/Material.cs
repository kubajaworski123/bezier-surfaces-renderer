using System.Numerics;

namespace Projekt2GK.Model
{
    /// <summary>
    /// Materiał powierzchni - ustawiany niezależnie dla każdej powierzchni.
    /// Przechowuje oba źródła koloru (jednolity kolor i teksturę), żeby przełączanie radiobuttonów
    /// nie gubiło wcześniej wybranego koloru ani wczytanej tekstury.
    /// </summary>
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

        /// <summary>Czy kolor obiektu pochodzi z tekstury (zamiast jednolitego koloru).</summary>
        public bool UseTexture { get; set; }

        public NormalMap? NormalMap { get; set; }

        /// <summary>Czy wektor normalny jest modyfikowany mapą normalnych.</summary>
        public bool UseNormalMap { get; set; }

        /// <summary>Aktywne źródło koloru obiektu I_O.</summary>
        public IColorSource ColorSource =>
            UseTexture && TextureSource is not null ? TextureSource : SolidColor;

        /// <summary>Aktywna mapa normalnych albo null, gdy modyfikacja normalnych jest wyłączona.</summary>
        public NormalMap? ActiveNormalMap => UseNormalMap ? NormalMap : null;
    }
}
