using System.Drawing;

namespace OuterZone.Entities.Scenes
{
    class DeathScene : Scene
    {
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

            base.Draw(g);
        }
    }
}
