namespace Projekt2GK.Rendering
{
    /// <summary>
    /// Bufor głębokości (Z-bufor). Obserwator patrzy z kierunku +z (V = [0, 0, 1]),
    /// więc bliżej obserwatora znaczy: większe z.
    /// </summary>
    public class DepthBuffer
    {
        public DepthBuffer(int width, int height)
        {
            Values = new float[width * height];
        }

        /// <summary>Głębokości pikseli, wiersz po wierszu (ten sam układ co w <see cref="FrameBuffer"/>).</summary>
        public float[] Values { get; }

        /// <summary>Na początku klatki żaden piksel nie jest niczym zasłonięty.</summary>
        public void Clear() => Array.Fill(Values, float.NegativeInfinity);

        /// <summary>
        /// Jeśli punkt o głębokości z jest bliżej obserwatora niż dotychczas narysowany w tym pikselu,
        /// zapisuje z i zwraca true (piksel należy narysować).
        /// </summary>
        public bool TryUpdate(int index, float z)
        {
            if (z <= Values[index])
                return false;

            Values[index] = z;
            return true;
        }
    }
}
