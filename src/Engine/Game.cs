using System;
using SquareWorld.Frontend;

namespace SquareWorld.Engine
{
    public class Game 
    {
        private readonly World _world;

        public int WorldSize { get; }

        public Game()
        {
            WorldSize = 10;
            _world = new World(WorldSize);
        }

        public void Render(GameObjectRenderer renderer)
        {
            _world.Render(renderer);
        }
    }
}