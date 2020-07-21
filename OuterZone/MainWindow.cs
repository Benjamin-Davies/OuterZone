using System;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using OuterZone.Entities;

namespace OuterZone
{
    public partial class MainWindow : Form
    {
        readonly Explorer explorer = new Explorer();
        readonly Floor floor = new Floor();
        readonly Stopwatch frameStopwatch = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();

            floor.Position += (0, 10);
            explorer.Velocity += (0.5, 0);
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                case Keys.Left:
                    explorer.Left = true;
                    break;
                case Keys.D:
                case Keys.Right:
                    explorer.Right = true;
                    break;
                case Keys.Space:
                case Keys.Up:
                    explorer.Jump();
                    break;
            }
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                case Keys.Left:
                    explorer.Left = false;
                    break;
                case Keys.D:
                case Keys.Right:
                    explorer.Right = false;
                    break;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            frameStopwatch.Start();
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            double dt = 1.5 * frameStopwatch.Elapsed.TotalSeconds;
            frameStopwatch.Restart();

            var g = e.Graphics;
            var scale = ClientSize.Height / 12f;
            var matrix = new Matrix();
            matrix.Scale(scale, scale);
            g.Transform = matrix;

            floor.Generate((double)ClientSize.Width / scale);
            explorer.Update(dt);
            floor.Update(dt);
            explorer.CollideWith(floor);

            explorer.Draw(g);
            floor.Draw(g);
        }

        private void FrameTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
