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

        #region Constructors

        public GameCoordinates(Vector2 position)
        {
            X = (int)position.X;
            Y = (int)position.Y;
        }

        public GameCoordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public GameCoordinates()
        {
            X = 0;
            Y = 0;
        }

        #endregion

        #region Conversion methods

        public Vector2 GetScreenPosition()
        {
            return new Vector2((x+1) * (24*SCALE), (y+1) * (24*SCALE) ) ;
        }

        public static GameCoordinates ScreenPositionToGameCoortinates(Vector2 position)
        {
            //TODO: remove magic number
            return new GameCoordinates(((int)position.X) / (24 * SCALE)-1, ((int)position.Y) / (24 * SCALE)-1);
        }

        #endregion
    }
}
