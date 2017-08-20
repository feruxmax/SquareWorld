using System;
using SquareWorld.Frontend.GameObjects;
using SquareWorld.Engine.Models;

namespace SquareWorld.Engine.GameObjects
{
    public class HeroObject : BaseGameObject 
    {
        public HeroObject(int x, int y, GameObjectRenderer renderer)
            : this(new Point(x, y), renderer)
        {
        }
        public HeroObject(Point position, GameObjectRenderer renderer)
            : base(position, renderer)
        {
        }
        
        public override void Render()
        {
            _renderer.Render(Position.X, Position.Y);
        }
    }
}