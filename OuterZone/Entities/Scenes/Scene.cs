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

        public Scene(ISceneManager sceneManager)
        {
            SceneManager = sceneManager;
        }

        public virtual void KeyChange(Keys key, bool down) { }

        public virtual void MouseDown(Vector mousePosition)
        {
            mousePosition -= Size / 2;
            var scale = (float)Size.Y / 12;
            mousePosition /= scale;

            foreach (var child in Children)
            {
                if (child.GetType() == typeof(Button))
                {
                    ((Button) child).MouseDown(mousePosition);
                }
            }
        }

        public virtual void CheckHover(Vector mousePosition)
        {
            mousePosition -= Size / 2;
            var scale = (float)Size.Y / 12;
            mousePosition /= scale;

            foreach (var child in Children)
            {
                if (child.GetType() == typeof(Button))
                {
                    ((Button)child).CheckHover(mousePosition);
                }
            }
        }
    }
}
