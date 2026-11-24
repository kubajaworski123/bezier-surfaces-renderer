using System.Numerics;

namespace Projekt2GK.Interaction
{
    /// <summary>
    /// Obrót sceny przeciąganiem myszy (arcball). Każde przesunięcie kursora daje przyrost obrotu
    /// między poprzednią a bieżącą pozycją, składany z aktualną orientacją sceny.
    /// </summary>
    public class RotatingSceneState : IMouseState
    {
        private readonly InteractionContext context;
        private Vector3 previousSpherePoint;

        public RotatingSceneState(InteractionContext context, Point startPoint)
        {
            this.context = context;
            previousSpherePoint = ArcballRotation.MapToSphere(startPoint, context.CanvasSize());
        }

        public IMouseState OnMouseDown(MouseEventArgs e) => this;

        public IMouseState OnMouseMove(MouseEventArgs e)
        {
            Vector3 currentSpherePoint = ArcballRotation.MapToSphere(e.Location, context.CanvasSize());
            Quaternion delta = ArcballRotation.ComputeDelta(previousSpherePoint, currentSpherePoint);
            previousSpherePoint = currentSpherePoint;

            if (delta != Quaternion.Identity)
            {
                context.Scene.Rotate(delta);
                context.InvalidateView();
            }
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
