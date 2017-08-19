using System;

namespace SquareWorld.Frontend
{
    interface IFrontend : IDisposable
    {
        void Run(double updateRate);
    }
}