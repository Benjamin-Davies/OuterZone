using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace OuterZone
{
    public partial class MainWindow : Form
    {
        Explorer explorer = new Explorer();
        Stopwatch frameStopwatch = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            frameStopwatch.Start();
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            double dt = frameStopwatch.Elapsed.TotalSeconds;
            frameStopwatch.Restart();

            explorer.Update(dt);

            explorer.Draw(g);
        }

        private void FrameTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
