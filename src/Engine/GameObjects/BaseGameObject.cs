using System;
using SquareWorld.Frontend.GameObjects;
using SquareWorld.Engine.Models;

namespace SquareWorld.Engine.GameObjects
{
    public abstract class BaseGameObject : IGameObject
    {
        protected readonly GameObjectRenderer _renderer;
        public Point Position { get; set; }

        protected BaseGameObject(Point position, GameObjectRenderer renderer)
        {
            Position = position;
            _renderer = renderer;
        }

        public abstract void Render();
    }
}