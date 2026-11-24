namespace Projekt2GK.Interaction
{
    /// <summary>
    /// Przechowuje bieżący stan myszy i przekazuje mu zdarzenia z płótna.
    /// </summary>
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
