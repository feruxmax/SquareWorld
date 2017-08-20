using System;
using SquareWorld.Frontend;
using SquareWorld.Frontend.GameObjects;
using SquareWorld.Engine.Enums;

namespace SquareWorld.Engine
{
    public class Game 
    {
        private World _world;

        public int WorldSize { get; }

        public Game()
        {
            WorldSize = 10;
        }

        public void BuildWorld(GameObjectsFactory gameObjectsFactory)
        {
            _world = new World(gameObjectsFactory, WorldSize);
        }

        public void SendAction(Actions action)
        {
            _world.HeroAction(action);
        }

        public void Render()
        {
            _world.Render();
        }
    }
}