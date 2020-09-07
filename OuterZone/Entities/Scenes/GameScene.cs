using OuterZone.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OuterZone.Entities.Scenes
{
    class GameScene : Scene
    {
        readonly Explorer explorer = new Explorer();
        readonly Floor floor = new Floor();
        readonly Tutorial tutorial;
        readonly Background background;
        readonly SoundPlayer boingSound;
        readonly SoundPlayer plopSound;
        readonly SoundPlayer oofSound;
        bool wasTouchingGround;

        public int Score => (int)(explorer.Position.X * 10);

        public GameScene(ISceneManager sceneManager) : base(sceneManager)
        {
            floor.Position += (0, 10);

            background = new Background
            {
                Random = new Random(),
                OffCenter = 1,
            };
            Children.Add(background);

            Children.Add(explorer);
            Children.Add(floor);

            if (!Settings.Default.CompletedTutorial)
            {
                tutorial = new Tutorial(explorer);
            }

            boingSound = new SoundPlayer(Resources.Boing_sound);
            plopSound = new SoundPlayer(Resources.Plop);
            oofSound = new SoundPlayer(Resources.Minecraft_death_sound);
        }

        public override void Update(double dt)
        {
            dt *= 1.5;
            base.Update(dt);

            var scale = Size.Y / 12;

            floor.Generate(explorer.Position.X + Size.X / scale);
            explorer.CollideWith(floor);

            if (explorer.IsTouching && !wasTouchingGround)
            {
                plopSound.Play();
            }
            wasTouchingGround = explorer.IsTouching;

            if (explorer.Position.Y > 12)
            {
                oofSound.Play();
                SceneManager.NextScene(typeof(DeathScene));
            }

            background.PlayerPosition = explorer.Position.X;

            tutorial?.Update(dt);
        }

        public override void Draw(Graphics g)
        {
            var scale = (float)Size.Y / 12;

            var oldMatrix = g.Transform;
            var matrix = new Matrix();
            matrix.Scale(scale, scale);
            matrix.Translate((float)(3.0 - explorer.Position.X), 0);
            g.Transform = matrix;

            background.ScreenSize = Size / scale;
            background.Position = (explorer.Position.X - 3.0, 0);

            base.Draw(g);

            g.Transform = oldMatrix;

            var scoreFont = new Font(Font.FontFamily, 24f);
            g.DrawString($"Score: {Score}", scoreFont, Brushes.White, new PointF(20, 20));

            tutorial?.Draw(g);
        }

        public override void KeyChange(Keys key, bool down, bool shift)
        {
            switch (key)
            {
                case Keys.A:
                case Keys.Left:
                    explorer.Left = down;
                    break;
                case Keys.D:
                case Keys.Right:
                    explorer.Right = down;
                    break;
                case Keys.Space:
                case Keys.Up:
                    if (down)
                    {
                        boingSound.Play();
                        explorer.Jump();
                    }
                    break;
                case Keys.Escape:
                case Keys.Enter:
                    if (down)
                        SceneManager.NextScene(typeof(PauseScene));
                    break;
            }
        }
    }
}
