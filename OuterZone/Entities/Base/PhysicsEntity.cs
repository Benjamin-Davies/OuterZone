using System;
using System.Linq;

using static OuterZone.Vector;

namespace OuterZone.Entities.Base
{
    public abstract class PhysicsEntity : Entity
    {
        public Vector Velocity { get; set; } = Zero;
        public virtual Vector G => (0, 10);
        public virtual double FrictionFactor => 2;

        public bool IsTouching { get; protected set; } = false;

        public override void Update(double dt)
        {
            Velocity += dt * G;
            // Friction happens when we are touching something
            if (IsTouching) Velocity -= dt * FrictionFactor * Velocity;
            // If we are barely moving and touching something, then dont move
            if (IsTouching && Velocity.SizeSq < Small) Velocity = Zero;
            Position += dt * Velocity;

            IsTouching = false;
        }

        public void CollideWith(CollectionEntity entity)
        {
            foreach (var child in entity.Children)
            {
                CollideWith(child);
            }
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
