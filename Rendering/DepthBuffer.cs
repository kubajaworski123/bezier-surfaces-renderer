namespace Projekt2GK.Rendering
{
    public class DepthBuffer
    {
        public DepthBuffer(int width, int height)
        {
            Values = new float[width * height];
        }

        public float[] Values { get; }

        public void Clear() => Array.Fill(Values, float.NegativeInfinity);

        public bool TryUpdate(int index, float z)
        {
            if (z <= Values[index])
                return false;

            Values[index] = z;
            return true;
        }
    }
}
