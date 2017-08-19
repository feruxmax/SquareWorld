using System;
using SquareWorld.Frontend;

namespace SquareWorld.Engine
{
    public class Game 
    {
        private readonly World _world;

        public Game()
        {
            _world = new World(10);
        }

        public void Draw(GameObjectRenderer drawer)
        {
            _world.Draw(drawer);
        }
    }
}