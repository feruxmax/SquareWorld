using System;

namespace SquareWorld.Frontend
{
    interface IFrontend : IDisposable
    {
        GameObjectsFactory GameObjectsFactory {get; }
        void Run(double updateRate);
    }
}