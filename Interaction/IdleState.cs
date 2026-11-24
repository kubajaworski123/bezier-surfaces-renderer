using System.Numerics;
using Projekt2GK.Model;
using Projekt2GK.Utils;

namespace Projekt2GK.Interaction
{
    /// <summary>
    /// Żaden przycisk nie jest wciśnięty. Naciśnięcie lewego przycisku na punkcie kontrolnym
    /// krzywej światła rozpoczyna jego przeciąganie, a w każdym innym miejscu - obrót sceny.
    /// </summary>
    public class IdleState : IMouseState
    {
        /// <summary>Maksymalna odległość kursora od punktu kontrolnego (w pikselach), by go chwycić.</summary>
        private const float HitRadius = 8f;

        private readonly InteractionContext context;

        public IdleState(InteractionContext context)
        {
            this.context = context;
        }

        public IMouseState OnMouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return this;

            int? pointIndex = FindCurvePointAt(new Vector2(e.X, e.Y));
            if (pointIndex is { } index)
                return new DraggingCurvePointState(context, index);

            return new RotatingSceneState(context, e.Location);
        }

        public IMouseState OnMouseMove(MouseEventArgs e) => this;

        public IMouseState OnMouseUp(MouseEventArgs e) => this;

        /// <summary>Indeks najbliższego punktu kontrolnego krzywej światła w promieniu chwytu, albo null.</summary>
        private int? FindCurvePointAt(Vector2 cursor)
        {
            Light light = context.Scene.Light;
            Size size = context.CanvasSize();
            int? closest = null;
            float closestDistance = HitRadius;

            for (int i = 0; i < light.Path.ControlPoints.Count; i++)
            {
                Vector2 xy = light.Path.ControlPoints[i];
                Vector2 screen = ProjectionUtils.ToScreen(new Vector3(xy.X, xy.Y, light.Z), size.Width, size.Height);
                float distance = Vector2.Distance(cursor, screen);
                if (distance <= closestDistance)
                {
                    closest = i;
                    closestDistance = distance;
                }
            }

            return closest;
        }
    }
}
