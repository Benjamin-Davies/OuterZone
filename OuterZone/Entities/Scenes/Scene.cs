using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OuterZone.Entities.Base;

namespace OuterZone.Entities.Scenes
{
    public abstract class Scene : CollectionEntity
    {
        public readonly ISceneManager SceneManager;
        public override Vector Size => (Vector)SceneManager.ClientSize;
        public Font Font => MainWindow.DefaultFont;

        public Scene(ISceneManager sceneManager)
        {
            SceneManager = sceneManager;
        }

        public virtual void KeyChange(Keys key, bool down) { }
    }
}
