using Microsoft.SqlServer.Server;
using System;
using System.Drawing;

namespace OuterZone.Entities.Scenes
{
    public interface ISceneManager
    {
        void NextScene(Type scene, params object[] data);
        void NextScene(Scene scene);

        Size ClientSize { get; }
    }
}