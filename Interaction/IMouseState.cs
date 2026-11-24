namespace Projekt2GK.Interaction
{
    /// <summary>
    /// Stan obsługi myszy na płótnie (wzorzec State).
    /// Każda metoda zwraca stan, który ma obowiązywać po obsłużeniu zdarzenia.
    /// </summary>
    public interface IMouseState
    {
        IMouseState OnMouseDown(MouseEventArgs e);
        IMouseState OnMouseMove(MouseEventArgs e);
        IMouseState OnMouseUp(MouseEventArgs e);
    }
}
