using System.Diagnostics;
using System.Drawing;

namespace OuterZone
{
    class Explorer
    {
        public Vector Position = Vector.Zero;
        public Vector Velocity = Vector.Zero;
        public readonly Vector Size = (1, 2);

        public void Update(double dt)
        {
            Velocity.Y += 10 * dt;
            Position += dt * Velocity;
        }

        public void Draw(Graphics g)
        {
            var scale = g.ClipBounds.Height / 12.0;
            g.FillRectangle(Brushes.Red, new RectangleF(scale * Position, scale * Size));
        }
    }
}
