using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Pacman.GameObjects;

namespace Pacman.GameManager
{
    class GameCoordinates
    {
        //TODO: remove
        public const int SCALE = 1;

        #region Properties

        private int x;
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        private int y;
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        #endregion


        //TODO: remove magic number
        public GameCoordinates(Vector2 position)
        {
            X = ((int)position.X) % (24*SCALE);
            Y = ((int)position.Y) % (24*SCALE);
        }


        public GameCoordinates(int x, int y)
        {
            X = x % (24*SCALE);
            Y = y % (24*SCALE);
        }

        public Vector2 GetScreenPosition()
        {
            return new Vector2(x * (24*SCALE), y * (24*SCALE) ) ;
        }
    }
}
