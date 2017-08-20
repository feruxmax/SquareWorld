using System;
using SquareWorld.Frontend;
using SquareWorld.Engine.GameObjects;

namespace SquareWorld.Engine
{
    public class World
    {
        private readonly HeroObject hero;
        private readonly IGameObject[,] gameObjects;
        public World(int size)
        {
            gameObjects = new IGameObject[size, size];

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    if(i != size/2 && j != size/2)
                        gameObjects[i, j] = new VoidObject(i, j);
                    else
                    {
                        hero = new HeroObject(i, j);
                        gameObjects[i, j] = hero;
                    }
                }
            
        }

        public void Render(GameObjectRenderer renderer)
        {
            foreach(var gameObject in gameObjects)
            {
                gameObject.Render(renderer);
            }
        }
    }
}