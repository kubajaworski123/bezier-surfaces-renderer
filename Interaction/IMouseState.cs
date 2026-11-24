namespace Projekt2GK.Interaction
{
    public interface IMouseState
    {
        IMouseState OnMouseDown(MouseEventArgs e);
        IMouseState OnMouseMove(MouseEventArgs e);
        IMouseState OnMouseUp(MouseEventArgs e);
    }
}
