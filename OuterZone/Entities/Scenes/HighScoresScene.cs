using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OuterZone.Entities.Scenes
{
    class HighScoresScene : Scene
    {
        private string Message => "High Scores";
        private string LoadingText => "Loading...";

        private readonly Button ExitToMenuButton;

        private const int HighScoreCount = 5;
        private List<HighScore> HighScores;

        public HighScoresScene(ISceneManager sceneManager) : base(sceneManager)
        {
            ExitToMenuButton = new Button
            {
                Text = "Exit to Menu",
                Position = (-4, 2.5),
            };
            ExitToMenuButton.OnClick += ExitToMenuButton_OnClick;
            Children.Add(ExitToMenuButton);

            Task.Run(async () =>
            {
                HighScores = await OuterZone.HighScores.DefaultInstance.GetHighScores();
                HighScores.Sort((a, b) => b.Score - a.Score);
            });
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

            var scoreFont = new Font(Font.FontFamily, 0.5f);
            if (HighScores != null)
            {
                for (int i = 0; i < HighScores.Count && i < HighScoreCount; i++)
                {
                    g.DrawString(HighScores[i].Username, scoreFont, Brushes.WhiteSmoke, new PointF(-4, -1.7f + 0.7f * i));
                    g.DrawString(HighScores[i].Score.ToString(), scoreFont, Brushes.WhiteSmoke, new PointF(2.5f, -1.7f + 0.7f * i));
                }
            }
            else
            {
                var loadingSize = g.MeasureString(LoadingText, scoreFont);
                g.DrawString(LoadingText, scoreFont, Brushes.WhiteSmoke, new PointF(loadingSize.Width / -2, -1.7f));
            }

            base.Draw(g);

            g.Transform = oldMatrix;
        }
    }
}
