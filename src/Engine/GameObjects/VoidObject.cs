using System;
using SquareWorld.Frontend.GameObjects;
using SquareWorld.Engine.Models;

namespace SquareWorld.Engine.GameObjects
{
    public class VoidObject : BaseGameObject
    {
        public VoidObject(int x, int y, GameObjectRenderer renderer)
            : this(new Point(x, y), renderer)
        {
        }

        public VoidObject(Point position, GameObjectRenderer renderer)
            : base(position, renderer)
        {
        }

        public override void Render()
        {
            _renderer.Render(Position.X, Position.Y);
        }
    }
}