namespace Projekt2GK.Rendering
{
    public class RenderSettings
    {
        public int TriangulationSteps { get; set; } = 15;
        public bool ShowBezierPolygon { get; set; } = true;
        public bool ShowTriangleMesh { get; set; } = false;
        public bool ShowFilledTriangles { get; set; } = true;

        public float Kd { get; set; } = 0.5f;

        public float Ks { get; set; } = 0.5f;

        public int M { get; set; } = 20;
    }
}
