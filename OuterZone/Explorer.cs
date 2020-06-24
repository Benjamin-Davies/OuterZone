using System.Diagnostics;
using System.Drawing;

namespace OuterZone
{
    class Explorer
    {
        public Vector Position = Vector.Zero;
        public Vector Velocity = Vector.Zero;
        public readonly Vector Size = (100, 150);

        public void Update(double dt)
        {
            Velocity.Y += 1000 * dt;
            Position += dt * Velocity;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Red, new RectangleF(Position, Size));
        }
    }
}
