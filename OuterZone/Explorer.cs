using System.Drawing;

namespace OuterZone
{
    class Explorer : Entity
    {
        public Vector Velocity = Vector.Zero;
        public override Vector Size => (1, 2);
        public override Brush Fill => Brushes.Red;

        public override void Update(double dt)
        {
            Velocity.Y += 10 * dt;
            Position += dt * Velocity;
        }
    }
}
