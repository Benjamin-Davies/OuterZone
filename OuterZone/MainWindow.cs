using System;
using System.CodeDom;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using OuterZone.Entities;
using OuterZone.Entities.Scenes;

namespace OuterZone
{
    public partial class MainWindow : Form, ISceneManager
    {
        readonly Stopwatch frameStopwatch = new Stopwatch();
        Scene currentScene;

        public MainWindow()
        {
            InitializeComponent();

            NextScene(typeof(GameScene));
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                case Keys.Left:
                    ((GameScene) currentScene).explorer.Left = true;
                    break;
                case Keys.D:
                case Keys.Right:
                    ((GameScene) currentScene).explorer.Right = true;
                    break;
                case Keys.Space:
                case Keys.Up:
                    ((GameScene) currentScene).explorer.Jump();
                    break;
            }
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                case Keys.Left:
                    ((GameScene) currentScene).explorer.Left = false;
                    break;
                case Keys.D:
                case Keys.Right:
                    ((GameScene) currentScene).explorer.Right = false;
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

            currentScene.Update(dt);
            currentScene.Draw(g);
        }

        private void FrameTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        public void NextScene(Type scene, params object[] data)
        {
            var constructor = scene.GetConstructors().First();
            var dataEnumerator = data.GetEnumerator();
            var parameters = constructor.GetParameters().Select(param =>
            {
                if (param.ParameterType == typeof(ISceneManager))
                    return this;
                dataEnumerator.MoveNext();
                return dataEnumerator.Current;
            }).ToArray();
            currentScene = constructor.Invoke(parameters) as Scene;
        }
    }
}
