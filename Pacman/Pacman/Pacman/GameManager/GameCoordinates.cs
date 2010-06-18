using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman.GameManager
{
    class GameCoordinates
    {
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
        GameCoordinates(Vector2 position)
        {
            X = ((int)position.X) % 24;
            Y = ((int)position.Y) % 24;

        }
    }
}
