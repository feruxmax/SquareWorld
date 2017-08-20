using System;
using SquareWorld.Frontend;
using SquareWorld.Engine.Models;

namespace SquareWorld.Engine.GameObjects
{
    public class HeroObject : BaseGameObject 
    {
        public HeroObject(int x, int y)
            : this(new Point(x, y))
        {
        }
        public HeroObject(Point position)
            : base(position)
        {
        }
        
        public override void Render(GameObjectRenderer renderer)
        {
            renderer.Render(0, Position.X, Position.Y);
        }
    }
}