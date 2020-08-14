using System;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        public readonly double JumpAnimationDuration = 0.5;

        public bool Left { get; set; }
        public bool Right { get; set; }

        private bool JumpAnimationActive = false;
        private double JumpAnimationTime = 0;

        public override void Update(double dt)
        {
            Walk(dt);

            JumpAnimationTime += dt;
            if (JumpAnimationTime > JumpAnimationDuration)
            {
                JumpAnimationActive = false;
            }

            base.Update(dt);
        }

        public override void Draw(Graphics g)
        {
            if (JumpAnimationActive)
            {
                var oldMatrix = g.Transform;
                var matrix = oldMatrix.Clone();
                var center = Position + Size / 2;
                double animationProgress = JumpAnimationTime / JumpAnimationDuration;
                animationProgress = EaseInOut(animationProgress);
                matrix.Translate((float)center.X, (float)center.Y);
                matrix.Rotate((float)(180 * animationProgress));
                matrix.Translate(-(float)center.X, -(float)center.Y);
                g.Transform = matrix;

                base.Draw(g);

                g.Transform = oldMatrix;
            }
            else
            {
                base.Draw(g);
            }
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

                // Begin the jump animation
                JumpAnimationTime = 0;
                JumpAnimationActive = true;
            }
        }

        private double EaseInOut(double x) => 3*x*x - 2*x*x*x;
    }
}
