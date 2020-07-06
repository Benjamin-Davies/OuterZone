using System;
using System.Drawing;
using System.Linq;

using static OuterZone.Vector;

namespace OuterZone
{
    class Explorer : Entity
    {
        public override Vector Size => (1, 2);
        public override Brush Fill => Brushes.Red;

        public Vector Velocity = Zero;
        public Vector G = (0, 10);
        public double FrictionFactor = 2;

        public bool IsTouching = false;

        public override void Update(double dt)
        {
            Velocity += dt * G;
            if (IsTouching) Velocity -= dt * FrictionFactor * Velocity;
            if (Velocity.SizeSq < Small * Small) Velocity = Zero;
            Position += dt * Velocity;

            IsTouching = false;
        }

        public void CollideWith(Entity entity)
        {
            if (Rectangle.IntersectsWith(entity.Rectangle))
            {
                // Calculate the overlap in each direction,
                // capping the values at zero
                double top = Math.Max(entity.Rectangle.Bottom - Rectangle.Top, 0);
                double right = Math.Max(Rectangle.Right - entity.Rectangle.Left, 0);
                double bottom = Math.Max(Rectangle.Bottom - entity.Rectangle.Top, 0);
                double left = Math.Max(entity.Rectangle.Right - Rectangle.Left, 0);

                // Find the smallest one
                double min = Min(top, right, bottom, left);

                // Only push away in the smallest direction
                if (top == min)
                    CollideWithEdge(entity.Rectangle.Bottom, Rectangle.Top, (0, -1));
                if (right == min)
                    CollideWithEdge(Rectangle.Right, entity.Rectangle.Left, (1, 0));
                if (bottom == min)
                    CollideWithEdge(Rectangle.Bottom, entity.Rectangle.Top, (0, 1));
                if (left == min)
                    CollideWithEdge(entity.Rectangle.Right, Rectangle.Left, (-1, 0));

                IsTouching = true;
            }
        }

        protected double Min(params double[] vals) => vals.Aggregate(Math.Min);

        protected void CollideWithEdge(double edge0, double edge1, Vector direction)
        {
            if (edge0 > edge1)
            {
                double amount = edge0 - edge1;

                var displacement = -amount * direction;
                Position += displacement;

                double velocityNormal = Dot(Velocity, direction);
                if (velocityNormal > 0)
                {
                    var velocityDisplacement = -velocityNormal * direction;
                    Velocity += velocityDisplacement;
                }
            }
        }
    }
}
