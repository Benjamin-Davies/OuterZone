using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using OuterZone.Entities.Base;

namespace OuterZone.Entities.Scenes
{
    abstract class Scene<T> : CollectionEntity
    {
        public readonly ISceneManager SceneManager;

        public Scene(ISceneManager sceneManager)
        {
            SceneManager = sceneManager;
        }

        public abstract Scene<T> CreateScene(T data);
    }
}
