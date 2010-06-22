using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Pacman.GameObjects;
using GameStateManagement;

namespace Pacman.GameManager
{
    class BoardFactory
    {

        static XmlDocument document;

        Board board;

        XmlNode wall;

        public BoardFactory()
        {
            

            document = new XmlDocument();
            document.Load(OptionsMenuScreen.PathToBoard);

            String name = document.DocumentElement.GetAttribute("name");


            board = new Board(name);


            XmlNodeList horizontalWallsNode = document.GetElementsByTagName("HorizontalWalls");
            if (horizontalWallsNode.Count > 0)
            {
                XmlNode horizontalWallsList = horizontalWallsNode.Item(0);


                for (int i = 0; i < horizontalWallsList.ChildNodes.Count; i++)
                {
                    wall = horizontalWallsList.ChildNodes[i];

                    string yString = wall.Attributes["y"].Value;
                    int y = Int32.Parse(yString);

                    string startXString = wall.Attributes["startX"].Value;
                    int startX = Int32.Parse(startXString);

                    string endXString = wall.Attributes["endX"].Value;
                    int endX = Int32.Parse(endXString);

                    board.addWall(new HorizontalWallGameObject(y, startX, endX));

                }
            }

            XmlNodeList verticalWallsNode = document.GetElementsByTagName("VerticalWalls");

            if (verticalWallsNode.Count > 0)
            {
                XmlNode verticalWallsList = verticalWallsNode.Item(0);


                for (int i = 0; i < verticalWallsList.ChildNodes.Count; i++)
                {
                    wall = verticalWallsList.ChildNodes[i];

                    string xString = wall.Attributes["x"].Value;
                    int x = Int32.Parse(xString);

                    string startYString = wall.Attributes["startY"].Value;
                    int startY = Int32.Parse(startYString);

                    string endYString = wall.Attributes["endY"].Value;
                    int endY = Int32.Parse(endYString);

                    board.addWall(new VerticalWallGameObject(x, startY, endY));

                }

            }

        }


        internal Board getBoard()
        {
            return board;
        }
    }
}
