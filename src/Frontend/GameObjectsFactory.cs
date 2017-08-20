using System;
using System.Collections;
using System.Collections.Generic;
using SquareWorld.Engine.GameObjects;
using SquareWorld.Frontend.GameObjects;

namespace SquareWorld.Frontend
{
    public class GameObjectsFactory
    {
        public List<GameObjectRenderer> Renderers {get; private set;}

        public GameObjectsFactory(int program)
        {
            Renderers = new List<GameObjectRenderer>()
            {
                new VoidObjectRenderer(program),
                new HeroObjectRenderer(program),
            };
        }

        public VoidObject CreateVoid(int x, int y)
        {
            return new VoidObject(x, y, Renderers[0]);
        }

        public HeroObject CreateHero(int x, int y)
        {
            return new HeroObject(x, y, Renderers[1]);
        }
    }
}
