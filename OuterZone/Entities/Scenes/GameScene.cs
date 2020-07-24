using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterZone.Entities.Scenes
{
    class GameScene : Scene
    {
        public readonly Explorer explorer = new Explorer();
        readonly Floor floor = new Floor();

        public GameScene(ISceneManager sceneManager) : base(sceneManager)
        {
            floor.Position += (0, 10);

            Children.Add(explorer);
            Children.Add(floor);
        }

        public override void Update(double dt)
        {
            base.Update(dt);

            var scale = SceneManager.ClientSize.Height / 12f;

            floor.Generate(explorer.Position.X + (double)SceneManager.ClientSize.Width / scale);
            explorer.CollideWith(floor);
        }

        public override void Draw(Graphics g)
        {
            var scale = SceneManager.ClientSize.Height / 12f;

            var matrix = new Matrix();
            matrix.Scale(scale, scale);
            matrix.Translate((float)(3.0 - explorer.Position.X), 0);
            g.Transform = matrix;

            base.Draw(g);
        }
    }
}
