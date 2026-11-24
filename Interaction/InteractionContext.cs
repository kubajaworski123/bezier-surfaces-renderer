using Projekt2GK.Model;

namespace Projekt2GK.Interaction
{
    /// <summary>
    /// Dane potrzebne stanom myszy: scena, rozmiar płótna i sposób oznaczenia obrazu jako nieaktualnego.
    /// </summary>
    public class InteractionContext
    {
        public InteractionContext(Scene scene, Func<Size> canvasSize, Action invalidateView)
        {
            Scene = scene;
            CanvasSize = canvasSize;
            InvalidateView = invalidateView;
        }

        public Scene Scene { get; }
        public Func<Size> CanvasSize { get; }
        public Action InvalidateView { get; }
    }
}
