using System;
using SquareWorld.Frontend;
using SquareWorld.Engine.Models;

namespace SquareWorld.Engine.GameObjects
{
    public abstract class BaseGameObject : IGameObject
    {
        public Point Position { get; set; }

        public BaseGameObject(Point position)
        {
            Position = position;
        }

        public abstract void Render(GameObjectRenderer renderer);
    }
}