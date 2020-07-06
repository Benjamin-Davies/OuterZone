using System;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace OuterZone
{
    public partial class MainWindow : Form
    {
        readonly Explorer explorer = new Explorer();
        readonly FloorSection floorSection = new FloorSection();
        readonly Stopwatch frameStopwatch = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();

            explorer.Velocity += (0.5, 0);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            frameStopwatch.Start();
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            double dt = frameStopwatch.Elapsed.TotalSeconds;
            frameStopwatch.Restart();

            var g = e.Graphics;
            var scale = ClientSize.Height / 12f;
            var matrix = new Matrix();
            matrix.Scale(scale, scale);
            g.Transform = matrix;

            explorer.Update(dt);
            floorSection.Update(dt);
            explorer.CollideWith(floorSection);

            explorer.Draw(g);
            floorSection.Draw(g);
        }

        private void FrameTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
