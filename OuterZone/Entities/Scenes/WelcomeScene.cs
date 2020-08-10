using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace OuterZone.Entities.Scenes
{
    class WelcomeScene : Scene
    {
        private string Message => Assembly.GetAssembly(GetType()).GetName().Name;

        private readonly Button PlayButton;
        private readonly Button SettingsButton;
        private readonly Button QuitGameButton;

        public WelcomeScene(ISceneManager sceneManager) : base(sceneManager)
        {
            PlayButton = new Button
            {
                Text = "Play",
                Position = (-4, 0),
            };
            PlayButton.OnClick += PlayButton_OnClick;
            Children.Add(PlayButton);

            SettingsButton = new Button
            {
                Text = "Settings",
                Position = (-4, 1.5),
            };
            SettingsButton.OnClick += SettingsButton_OnClick;
            Children.Add(SettingsButton);

            QuitGameButton = new Button
            {
                Text = "Quit Game",
                Position = (-4, 3),
            };
            QuitGameButton.OnClick += QuitGameButton_OnClick;
            Children.Add(QuitGameButton);
        }

        private void SettingsButton_OnClick(object sender, Vector e)
        {
            SceneManager.NextScene(typeof(SettingsScene));
        }

        private void QuitGameButton_OnClick(object sender, Vector e)
        {
            Application.Exit();
        }

        private void PlayButton_OnClick(object sender, Vector e)
        {
            SceneManager.NextScene(typeof(GameScene));
        }

        public override void Draw(Graphics g)
        {
            var scale = (float)Size.Y / 12;
            var oldMatrix = g.Transform;
            var matrix = new Matrix();
            matrix.Translate((float)(Size.X / 2), (float)(Size.Y / 2));
            matrix.Scale(scale, scale);
            g.Transform = matrix;


            var titleFont = new Font(Font.FontFamily, 1);
            var titleSize = g.MeasureString(Message, titleFont);
            g.DrawString(Message, titleFont, Brushes.White, new PointF(titleSize.Width / -2, -2 - titleSize.Height));

            base.Draw(g);

            g.Transform = oldMatrix;
        }
    }
}
