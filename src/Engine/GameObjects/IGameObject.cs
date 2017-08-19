using System;
using SquareWorld.Frontend;
using SquareWorld.Engine.Models;

namespace SquareWorld.Engine.GameObjects
{
    interface IGameObject
    {
        Point Position { get; set; }

        void Draw(GameObjectRenderer drawer);
    }
}