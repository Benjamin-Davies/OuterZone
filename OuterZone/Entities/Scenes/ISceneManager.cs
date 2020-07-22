using Microsoft.SqlServer.Server;

namespace OuterZone.Entities.Scenes
{
    interface ISceneManager
    {
        void NextScene<S>() where S : Scene<object>;
        void NextScene<S, T>(T data) where S : Scene<T>;
    }
}