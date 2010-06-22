using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.GameObjects;
using Microsoft.Xna.Framework;

namespace Pacman.GameManager
{
    class CollisionManager
    {
        #region Fields

        List<GameObject> walls;
        List<GameObject> portals;
        List<GameObject> monsterHouses;

        List<GameObject> monsters;
        List<GameObject> dots;
        List<GameObject> pills;

        List<GameObject> fruits;
        List<GameObject> pacmans;

        #endregion

        #region Initialization

        static CollisionManager instance;


        public static CollisionManager getInstance()
        {
            if (instance == null)
                instance = new CollisionManager();

            return instance;
        }

        private CollisionManager()
        { }



        public void Initialize(List<GameObject> walls, List<GameObject> portals, List<GameObject> monsterHouses,
                                List<GameObject> dots, List<GameObject> pills, List<GameObject> fruits,
                                List<GameObject> pacmans, List<GameObject>monsters)
        {
            this.walls = walls;
            this.portals = portals;
            this.monsterHouses = monsterHouses;
            this.dots = dots;
            this.pills = pills;
            this.fruits = fruits;
            this.pacmans = pacmans;
            this.monsters = monsters;
        }

        #endregion

        #region Checking collision

        public bool IsCollision(Rectangle rect, PacmanGameObject.direction direction, GameObject.GameObjectType gameObjectTyp)
        {
            switch (gameObjectTyp)
            { 
                case GameObject.GameObjectType.WALLS:
                    return IsPacmanMakeCollisionWithWalls(rect, direction);
                case GameObject.GameObjectType.DOTS:
                    return IsPacmanMakeCollisionWithDots(rect, direction);
                case GameObject.GameObjectType.PILLS:
                    return IsPacmanMakeCollisionWithPills(rect, direction);

                case GameObject.GameObjectType.GHOSTS:



                    goto case GameObject.GameObjectType.MONSTERS;
                case GameObject.GameObjectType.MONSTERS:
                    return IsPacmanMakeCollisionWithMonsters(rect, direction);


            }

            return false;
        }

        private bool IsPacmanMakeCollisionWithPills(Rectangle pacmanRect, MoveableGameObject.direction direction)
        {
            int radius = 8;



            Rectangle pacmanMiniRect = new Rectangle(pacmanRect.X + (GameObject.OBJECT_SIZE / 2) - radius,
                                                     pacmanRect.Y + (GameObject.OBJECT_SIZE / 2) - radius,
                                                     2 * radius,
                                                     2 * radius);

            foreach (MagicPillGameObject pill in pills)
            {
                if (pacmanMiniRect.Intersects(pill.ScreenRectangle))
                {

                    pill.Clear();
                    return true;
                }

            }

            return false;

        }

        #region Pacman vs. Others

        private bool IsPacmanMakeCollisionWithWalls(Rectangle pacmanRect, PacmanGameObject.direction direction)
        {
            /*Rectangle pacmanRect = new Rectangle((int)(pacman.ScreenPosition.X + move.X),
                                                 (int)(pacman.ScreenPosition.Y + move.Y),
                                                 GameObject.OBJECT_SIZE,
                                                 GameObject.OBJECT_SIZE);
            */
            switch (direction)
            {
                #region with horizontal walls

                case PacmanGameObject.direction.Down: 
                    goto case PacmanGameObject.direction.Up;
                case PacmanGameObject.direction.Up:

                    foreach(GameObject wall in walls)
                    {
                        if(wall is HorizontalWallGameObject)
                        {
                            HorizontalWallGameObject horizontalWall = (HorizontalWallGameObject)wall;

                            Rectangle wallRect = new Rectangle((int)horizontalWall.Start.GetScreenPosition().X,
                                                               (int)horizontalWall.Start.GetScreenPosition().Y, 
                                                               GameObject.OBJECT_SIZE*horizontalWall.Length()
                                                               , 0);

                            if (pacmanRect.Intersects(wallRect))
                                return true;


                        }
                    }

                    return false;

                #endregion

                #region with vertical walls

                case PacmanGameObject.direction.Left:
                    goto case PacmanGameObject.direction.Right;
                case PacmanGameObject.direction.Right:

                    foreach (GameObject wall in walls)
                    {
                        if (wall is VerticalWallGameObject)
                        {
                            VerticalWallGameObject verticallWall = (VerticalWallGameObject)wall;

                            Rectangle wallRect = new Rectangle((int)verticallWall.Start.GetScreenPosition().X,
                                                               (int)verticallWall.Start.GetScreenPosition().Y,
                                                               0,
                                                               GameObject.OBJECT_SIZE * verticallWall.Length());

                            if (pacmanRect.Intersects(wallRect))
                                return true;


                        }
                    }

                    return false;

                #endregion
            }

            return false;
        }

        private bool IsPacmanMakeCollisionWithDots(Rectangle pacmanRect, PacmanGameObject.direction direction)
        {

            GameCoordinates pacEnvironmet = GameCoordinates.ScreenPositionToGameCoortinates(new Vector2(pacmanRect.X, pacmanRect.Y));

            List<DotGameObject> dotsToCheck = new List<DotGameObject>();

            dotsToCheck.Add(DotGameObject.array[pacEnvironmet.X, pacEnvironmet.Y]);

            if(pacEnvironmet.X < 24-1 /*BoardSize*/)
                dotsToCheck.Add(DotGameObject.array[pacEnvironmet.X + 1, pacEnvironmet.Y]);

            if (pacEnvironmet.Y < 18 - 1)
                dotsToCheck.Add(DotGameObject.array[pacEnvironmet.X, pacEnvironmet.Y + 1]);

            if (pacEnvironmet.X < 24-1 /*BoardSize*/ && pacEnvironmet.Y < 18-1)
                dotsToCheck.Add(DotGameObject.array[pacEnvironmet.X + 1, pacEnvironmet.Y + 1]);

            int radius = 3;

            Rectangle pacmanMiniRect = new Rectangle(pacmanRect.X + (GameObject.OBJECT_SIZE/2)-radius,
                                                     pacmanRect.Y + (GameObject.OBJECT_SIZE/2)-radius,
                                                     2*radius,
                                                     2*radius);

            foreach (DotGameObject dot in dotsToCheck)
            {
                if (pacmanMiniRect.Intersects(dot.ScreenRectangle))
                {

                    dot.Clear();
                    return true;
                }

            }


            return false;
        }

        private bool IsPacmanMakeCollisionWithMonsters(Rectangle pacmanRect, MoveableGameObject.direction direction)
        {
            foreach(MonsterGameObject monster in monsters)
            {
                if (pacmanRect.Intersects(monster.ScreenRectanglePosition))
                {
                    if (MonsterGameObject.Ghost)
                    {
                        monster.Kill();
                    }
                    else 
                    {
                        if(! monster.IsDead)
                            return true;
                    }
                
                }
                    
            
            }

            return false;
        }

        #endregion

        #endregion
    }
}
