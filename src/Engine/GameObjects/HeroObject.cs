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
        
        public override void Draw(GameObjectRenderer drawer)
        {
            drawer.Draw(0, Position.X, Position.Y);
        }
    }
}