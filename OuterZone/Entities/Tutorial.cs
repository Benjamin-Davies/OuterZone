using OuterZone.Entities.Base;
using OuterZone.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterZone.Entities
{
    class Tutorial : Entity
    {
        private readonly Explorer explorer;
        private State state = State.MoveRight;

        public Tutorial(Explorer explorer)
        {
            this.explorer = explorer;
        }

        public override void Update(double dt)
        {
            switch(state)
            {
                case State.MoveRight:
                    if (explorer.Position.X > 2)
                        state = State.MoveLeft;
                    break;
                case State.MoveLeft:
                    if (explorer.Position.X < 0)
                        state = State.Jump;
                    break;
                case State.Jump:
                    if (explorer.Position.Y < 6)
                        state = State.GoodJob;
                    break;
            }

            if (explorer.Position.X > 20)
            {
                state = State.Done;
                Settings.Default.CompletedTutorial = true;
            }
            else if (explorer.Position.X > 15)
            {
                state = State.Pause;
            }
        }

        public override void Draw(Graphics g)
        {
            var tipFont = new Font(Font.FontFamily, 48);
            var text = "";
            switch (state)
            {
                case State.MoveRight:
                    text = "Use the right arrow or 'D' to move right.";
                    break;
                case State.MoveLeft:
                    text = "Use the left arrow or 'A' to move left.";
                    break;
                case State.Jump:
                    text = "Use the up arrow or the spacebar to jump";
                    break;
                case State.GoodJob:
                    text = "Good Job!\nNow get as far right as possible,\nbut remember to jump over the holes!";
                    break;
                case State.Pause:
                    text = "You can press escape at any time to pause.";
                    break;
            }
            g.DrawString(text, tipFont, Brushes.White, new PointF(20, 100));
        }

        private enum State
        {
            MoveRight,
            MoveLeft,
            Jump,
            GoodJob,
            Pause,
            Done,
        }
    }
}
