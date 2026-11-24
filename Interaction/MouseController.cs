namespace Projekt2GK.Interaction
{
    public class MouseController
    {
        private IMouseState state;

        public MouseController(InteractionContext context)
        {
            state = new IdleState(context);
        }

        public void OnMouseDown(MouseEventArgs e) => state = state.OnMouseDown(e);
        public void OnMouseMove(MouseEventArgs e) => state = state.OnMouseMove(e);
        public void OnMouseUp(MouseEventArgs e) => state = state.OnMouseUp(e);
    }
}
