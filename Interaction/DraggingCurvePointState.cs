using System.Numerics;
using Projekt2GK.Utils;

namespace Projekt2GK.Interaction
{
    /// <summary>
    /// Przeciąganie punktu kontrolnego krzywej, po której porusza się światło.
    /// Krzywa leży w układzie obserwatora, więc pozycja kursora przekłada się wprost na x, y punktu.
    /// </summary>
    public class DraggingCurvePointState : IMouseState
    {
        private readonly InteractionContext context;
        private readonly int pointIndex;

        public DraggingCurvePointState(InteractionContext context, int pointIndex)
        {
            this.context = context;
            this.pointIndex = pointIndex;
        }

        public IMouseState OnMouseDown(MouseEventArgs e) => this;

        public IMouseState OnMouseMove(MouseEventArgs e)
        {
            Size size = context.CanvasSize();
            Vector2 scenePoint = ProjectionUtils.ToScene(new Vector2(e.X, e.Y), size.Width, size.Height);
            context.Scene.Light.Path.MoveControlPoint(pointIndex, scenePoint);
            context.InvalidateView();
            return this;
        }

        public IMouseState OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                return new IdleState(context);
            return this;
        }
    }
}
