namespace Projekt2GK.Utils
{
    /// <summary>
    /// Operacje na liczbach używane w wielu miejscach projektu.
    /// </summary>
    public static class MathUtils
    {
        /// <summary>
        /// Wielomian Bernsteina B(i, n)(t) = C(n, i) * t^i * (1 - t)^(n - i).
        /// </summary>
        public static float Bernstein(int n, int i, float t)
        {
            if (i < 0 || i > n)
                return 0f;

            return BinomialCoefficient(n, i) * MathF.Pow(t, i) * MathF.Pow(1f - t, n - i);
        }

        /// <summary>
        /// Wszystkie funkcje bazowe stopnia n w punkcie t: [B(0, n)(t), ..., B(n, n)(t)].
        /// </summary>
        public static float[] BernsteinBasis(int n, float t)
        {
            var result = new float[n + 1];
            for (int i = 0; i <= n; i++)
                result[i] = Bernstein(n, i, t);
            return result;
        }

        /// <summary>
        /// Symbol Newtona C(n, k).
        /// </summary>
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

        /// <summary>
        /// Potęga o wykładniku naturalnym, przez podnoszenie do kwadratu:
        /// x^m = (x^2)^(m/2) dla parzystego m, x · x^(m-1) dla nieparzystego.
        /// Dla m ≤ 100 to najwyżej ~13 mnożeń - szybciej niż ogólne MathF.Pow (logarytm i exp).
        /// </summary>
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
