namespace Projekt2GK.Rendering.Stages
{
    /// <summary>
    /// Etap potoku renderowania. Etapy działają na całej klatce, nigdy na pojedynczym pikselu.
    /// </summary>
    public interface IRenderStage
    {
        /// <summary>Czy etap ma się wykonać w tej klatce (np. zależnie od checkboxa).</summary>
        bool IsEnabled(RenderContext context);

        void Execute(RenderContext context);
    }
}
