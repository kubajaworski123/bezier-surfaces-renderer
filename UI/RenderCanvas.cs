using System.ComponentModel;
using Projekt2GK.Rendering;

namespace Projekt2GK.UI
{
    public class RenderCanvas : Control
    {
        public RenderCanvas()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SceneRenderer? Renderer { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Renderer is null)
            {
                e.Graphics.Clear(BackColor);
                return;
            }

            Renderer.Render();
            e.Graphics.DrawImageUnscaled(Renderer.Output, 0, 0);
        }
    }
}
