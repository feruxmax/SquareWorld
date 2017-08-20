using System;
using SquareWorld.Frontend.GameObjects;
using SquareWorld.Engine.Models;

namespace SquareWorld.Engine.GameObjects
{
    interface IGameObject
    {
        Point Position { get; set; }

        void Render();
    }
}