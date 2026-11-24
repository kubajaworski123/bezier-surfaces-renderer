namespace Projekt2GK.Utils
{
    public static class MathUtils
    {
        public static float Bernstein(int n, int i, float t)
        {
            if (i < 0 || i > n)
                return 0f;

            return BinomialCoefficient(n, i) * MathF.Pow(t, i) * MathF.Pow(1f - t, n - i);
        }

        public static float[] BernsteinBasis(int n, float t)
        {
            var result = new float[n + 1];
            for (int i = 0; i <= n; i++)
                result[i] = Bernstein(n, i, t);
            return result;
        }

        public static int BinomialCoefficient(int n, int k)
        {
            if (k < 0 || k > n)
                return 0;

            k = Math.Min(k, n - k);
            int result = 1;
            for (int i = 1; i <= k; i++)
                result = result * (n - k + i) / i;
            return result;
        }

        public static float Clamp01(float value) => Math.Clamp(value, 0f, 1f);

        public static float IntPow(float x, int exponent)
        {
            float result = 1f;
            while (exponent > 0)
            {
                if ((exponent & 1) == 1)
                    result *= x;
                x *= x;
                exponent >>= 1;
            }
            return result;
        }
    }
}
