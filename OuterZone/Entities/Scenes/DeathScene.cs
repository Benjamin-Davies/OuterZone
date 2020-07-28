using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace OuterZone.Entities.Scenes
{
    class DeathScene : Scene
    {
        private static readonly string Message = "You Died";

        private readonly Scene GameScene;

        public DeathScene(ISceneManager sceneManager, Scene gameScene) : base(sceneManager)
        {
            GameScene = gameScene;
        }

        public override void Draw(Graphics g)
        {
            GameScene.Draw(g);

            var background = new SolidBrush(Color.FromArgb(127, Color.Crimson));
            g.FillRectangle(background, Rectangle);

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
