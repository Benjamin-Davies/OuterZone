using OuterZone.Properties;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace OuterZone.Entities.Scenes
{
    class SettingsScene : Scene
    {
        private string Message => "Change Username";

        private readonly TextInput UsernameInput;
        private readonly Button ExitToMenuButton;

        public SettingsScene(ISceneManager sceneManager) : base(sceneManager)
        {
            UsernameInput = new TextInput
            {
                Text = Settings.Default.Username,
                Position = (-4, -1),
            };
            UsernameInput.OnChange += UsernameInput_OnChange;
            Children.Add(UsernameInput);

            ExitToMenuButton = new Button
            {
                Text = "Exit to Menu",
                Position = (-4, 3),
            };
            ExitToMenuButton.OnClick += ExitToMenuButton_OnClick;
            Children.Add(ExitToMenuButton);
        }

        private void UsernameInput_OnChange(object sender, string e)
        {
            Settings.Default.Username = e;
        }

        private void ExitToMenuButton_OnClick(object sender, Vector e)
        {
            SceneManager.NextScene(typeof(WelcomeScene));
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
