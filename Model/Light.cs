using System.Numerics;

namespace Projekt2GK.Model
{
    public class Light
    {
        public const float LapDurationSeconds = 8f;

        public LightPath Path { get; } = new();

        public float T { get; private set; }

        public float Z { get; set; } = 2f;

        public Vector3 Color { get; set; } = Vector3.One;

        public Vector3 Position
        {
            get
            {
                Vector2 xy = Path.Evaluate(T);
                return new Vector3(xy.X, xy.Y, Z);
            }
        }

        public void Advance(float elapsedSeconds)
        {
            T = (T + elapsedSeconds / LapDurationSeconds) % 1f;
        }
    }
}
