using System.Numerics;

namespace Projekt2GK.Model
{
    /// <summary>
    /// Punktowe źródło światła poruszające się po zamkniętej krzywej Béziera na płaszczyźnie z = Z.
    /// Pozycja jest w układzie obserwatora - nie obraca się razem ze sceną.
    /// </summary>
    public class Light
    {
        /// <summary>Czas jednego okrążenia krzywej podczas animacji.</summary>
        public const float LapDurationSeconds = 8f;

        public LightPath Path { get; } = new();

        /// <summary>Parametr krzywej t z [0, 1) wyznaczający aktualne położenie światła.</summary>
        public float T { get; private set; }

        /// <summary>Wysokość płaszczyzny, na której leży krzywa.</summary>
        public float Z { get; set; } = 2f;

        /// <summary>Kolor światła I_L, RGB z [0, 1]; domyślnie biały.</summary>
        public Vector3 Color { get; set; } = Vector3.One;

        public Vector3 Position
        {
            get
            {
                Vector2 xy = Path.Evaluate(T);
                return new Vector3(xy.X, xy.Y, Z);
            }
        }

        /// <summary>
        /// Przesuwa światło wzdłuż krzywej o czas <paramref name="elapsedSeconds"/>.
        /// Prędkość zależy od upływu czasu, a nie od liczby wywołań, więc nie zależy od liczby klatek na sekundę.
        /// </summary>
        public void Advance(float elapsedSeconds)
        {
            T = (T + elapsedSeconds / LapDurationSeconds) % 1f;
        }
    }
}
