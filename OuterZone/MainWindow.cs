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

            NextScene(typeof(WelcomeScene));
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            currentScene.KeyChange(e.KeyCode, true, e.Shift);

            switch (e.KeyCode)
            {
                case Keys.F11:
                    if (FormBorderStyle == FormBorderStyle.None)
                    {
                        FormBorderStyle = FormBorderStyle.Sizable;
                        WindowState = FormWindowState.Normal;
                    }
                    else
                    {
                        FormBorderStyle = FormBorderStyle.None;
                        WindowState = FormWindowState.Maximized;
                        Bounds = Screen.PrimaryScreen.Bounds;
                    }
                    break;
            }
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            currentScene.KeyChange(e.KeyCode, false, e.Shift);
        }

        private void MainWindow_MouseDown(object sender, MouseEventArgs e) => currentScene.MouseDown((Vector)e.Location);

        private void MainWindow_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            Bounds = Screen.PrimaryScreen.Bounds;

            frameStopwatch.Start();
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            double dt = frameStopwatch.Elapsed.TotalSeconds;
            frameStopwatch.Restart();

            var g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

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
                if (param.ParameterType == typeof(Scene))
                    return currentScene;

                dataEnumerator.MoveNext();
                return dataEnumerator.Current;
            }).ToArray();
            NextScene(constructor.Invoke(parameters) as Scene);
        }

        public void NextScene(Scene scene)
        {
            currentScene = scene;
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            currentScene.CheckHover((Vector)e.Location);
        }
    }
}
