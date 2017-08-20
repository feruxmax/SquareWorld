using System;
using SquareWorld.Frontend;
using SquareWorld.Engine.Models;

namespace SquareWorld.Engine.GameObjects
{
    public class VoidObject : BaseGameObject
    {
        public VoidObject(int x, int y)
            : this(new Point(x, y))
        {
        }

        public VoidObject(Point position)
            : base(position)
        {
        }

        public override void Render(GameObjectRenderer renderer)
        {
            renderer.Render(0, Position.X, Position.Y);
        }
    }
}