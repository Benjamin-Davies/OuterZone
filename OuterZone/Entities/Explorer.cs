using System.Drawing;

using static OuterZone.Vector;

namespace OuterZone.Entities
{
    class Explorer : Base.PhysicsEntity
    {
        public override Vector Size => (1, 2);
        public override Brush Fill => Brushes.Red;

        public readonly double WalkingSpeed = 10;
        public readonly double WalkingAcceleration = 50;
        public readonly double JumpSpeed = 10;

        public bool Left { get; set; }
        public bool Right { get; set; }

        public override void Update(double dt)
        {
            Walk(dt);

            base.Update(dt);
        }

        protected void Walk(double dt)
        {
            double desiredSpeed = 0;
            if (Left) desiredSpeed -= 1;
            if (Right) desiredSpeed += 1;
            desiredSpeed *= WalkingSpeed;

            var v = Velocity;
            double a = (desiredSpeed - v.X) / WalkingSpeed;
            v.X += dt * WalkingAcceleration * a;
            Velocity = v;
        }

        public void Jump()
        {
            if (IsTouching)
            {
                Velocity += JumpSpeed * Up;
            }
        }
    }
}
