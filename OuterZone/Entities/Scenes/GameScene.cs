using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterZone.Entities.Scenes
{
    class GameScene : Scene<object>
    {
        public GameScene(ISceneManager sceneManager) : base(sceneManager) { }

        public override Scene<object> CreateScene(object data) => new GameScene(sceneManager);
    }
}
