namespace Projekt2GK.Rendering
{
    /// <summary>
    /// Ustawienia renderowania ustawiane z panelu sterowania.
    /// </summary>
    public class RenderSettings
    {
        public int TriangulationSteps { get; set; } = 15;
        public bool ShowBezierPolygon { get; set; } = true;
        public bool ShowTriangleMesh { get; set; } = false;
        public bool ShowFilledTriangles { get; set; } = true;

        /// <summary>Współczynnik składowej rozproszonej, [0, 1].</summary>
        public float Kd { get; set; } = 0.5f;

        /// <summary>Współczynnik składowej zwierciadlanej, [0, 1].</summary>
        public float Ks { get; set; } = 0.5f;

        /// <summary>Wykładnik zwierciadlany, [1, 100].</summary>
        public int M { get; set; } = 20;
    }
}
