using System;
using SquareWorld.Engine.Enums;
using SquareWorld.Frontend;
using SquareWorld.Frontend.GameObjects;
using SquareWorld.Engine.GameObjects;

namespace SquareWorld.Engine
{
    public class World
    {
        private readonly int _size;
        private readonly HeroObject hero;
        private readonly IGameObject[,] gameObjects;
        public World(GameObjectsFactory gameObjectsFactory, int size)
        {
            _size = size; 
            gameObjects = new IGameObject[size, size];

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    if(i != size/2 || j != size/2)
                        gameObjects[i, j] = gameObjectsFactory.CreateVoid(i, j);
                    else
                    {
                        hero = gameObjectsFactory.CreateHero(i, j);
                        gameObjects[i, j] = hero;
                    }
                }
            
        }

        public void Render()
        {
            foreach(var gameObject in gameObjects)
            {
                gameObject.Render();
            }
        }

        public void HeroAction(Actions action)
        {
            switch(action)
            {
                case Actions.Up:
                    if (hero.Position.Y < _size)
                    {
                        var position = hero.Position;
                        position.Y++;
                        hero.Position = position;
                    }
                    break;
                case Actions.Down:
                    if (hero.Position.Y > 0)
                    {
                        var position = hero.Position;
                        position.Y--;
                        hero.Position = position;
                    }
                    break;
                case Actions.Left:
                    if (hero.Position.X > 0)
                    {
                        var position = hero.Position;
                        position.X--;
                        hero.Position = position;
                    }
                    break;
                case Actions.Right:
                    if (hero.Position.X < _size)
                    {
                        var position = hero.Position;
                        position.X++;
                        hero.Position = position;
                    }
                    break;
            }
        }
    }
}