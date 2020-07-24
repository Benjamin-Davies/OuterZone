using Microsoft.SqlServer.Server;
using System;
using System.Drawing;

namespace OuterZone.Entities.Scenes
{
    public interface ISceneManager
    {
        void NextScene(Type scene, params object[] data);

        Size ClientSize { get; }
    }
}