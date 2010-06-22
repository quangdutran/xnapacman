using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.GameObjects;

namespace Pacman.GameManager
{
    class Board
    {
        #region Fields

        String name;

        List<GameObject> walls = new List<GameObject>();

        internal List<GameObject> Walls
        {
            get { return walls; }
            set { walls = value; }
        }

        #endregion

        public Board(String name)
        {
            this.name = name;
        }

        internal void addWall(WallGameObject horizontalWallGameObject)
        {
            walls.Add(horizontalWallGameObject);
        }
    }
}
