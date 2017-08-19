﻿using System;
using SquareWorld.Frontend;
using SquareWorld.Engine;

namespace SquareWorld
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var game = new Game();
            using (IFrontend frontend = new GL4Frontend(game))
            {
                frontend.Run(60.0);
            }
        }
    }
}
