﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagement;
using Microsoft.Xna.Framework.Input;
using Pacman.GameManager;

namespace Pacman.GameObjects
{
    class GameObjectManager
    {
        #region Statics

        static bool levelCompleted = false;
        static bool GameOver = false;

        #endregion

        #region Fields


        List<GameObject> walls;
        List<GameObject> portals = new List<GameObject>();
        List<GameObject> monsterHouses = new List<GameObject>();

        List<GameObject> monsters = new List<GameObject>();
        List<GameObject> dots = new List<GameObject>();
        List<GameObject> pills = new List<GameObject>();


        List<GameObject> fruits = new List<GameObject>();
        List<GameObject> pacmans = new List<GameObject>();

        List<List<GameObject>> listOfAllGameObjects = new List<List<GameObject>>();

        PacmanGameObject pacman;
        MonsterGameObject monsterHouse;
        ToolBarGameObject toolBarGameObject;

        CollisionManager collisionManager;

        Board board;

        #endregion

        #region Properties 

        ScreenManager screenManager;

        public ScreenManager ScreenManager
        {
            get { return screenManager; }
            set { screenManager = value; }
        }

        
        ContentManager contentManager = null;

        public ContentManager ContentManager
        {
            get { return contentManager; }
            set
            { 
                contentManager = value;
                GameObject.Content = contentManager;
            }
        }

        SpriteBatch spriteBatch;

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            set { 
                spriteBatch = value;
                GameObject.SpriteBatch = spriteBatch;
            }
        }

        #endregion

        #region Initialization

        public GameObjectManager()
        {
            BoardFactory boardFactory = new BoardFactory();
            board = boardFactory.getBoard();
            levelCompleted = false;
            GameOver = false;

        }


        public void LoadContent()
        {

            
            monsters.Add(new MonsterGameObject(MonsterGameObject.MonsterGameObjectColor.Blue));
            monsters.Add(new MonsterGameObject(MonsterGameObject.MonsterGameObjectColor.Green));
            monsters.Add(new MonsterGameObject(MonsterGameObject.MonsterGameObjectColor.Pink));
            monsters.Add(new MonsterGameObject(MonsterGameObject.MonsterGameObjectColor.Red));
            

            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;

            GameCoordinates topLeftArena = new GameCoordinates(0, 0);
            GameCoordinates bottomRightArena = new GameCoordinates();
            
            //TODO:remove magic number
            bottomRightArena.X = viewport.Width / 24 -2;
            bottomRightArena.Y = viewport.Height / 24 -2;

            walls = board.Walls;

            //borders
            if (
                //false
                true
                )
            {
                walls.Add(new HorizontalWallGameObject(topLeftArena.Y, topLeftArena.X, bottomRightArena.X));
                walls.Add(new HorizontalWallGameObject(bottomRightArena.Y, topLeftArena.X, bottomRightArena.X));

                walls.Add(new VerticalWallGameObject(topLeftArena.X, topLeftArena.Y, bottomRightArena.Y));
                walls.Add(new VerticalWallGameObject(bottomRightArena.X, topLeftArena.Y, bottomRightArena.Y));
            }

            dots = DotGameObject.Generate(bottomRightArena.X, bottomRightArena.Y);

           
            listOfAllGameObjects.Add(dots);

            pills = MagicPillGameObject.Generate();

            listOfAllGameObjects.Add(pills);
            listOfAllGameObjects.Add(walls);
            listOfAllGameObjects.Add(portals);
            listOfAllGameObjects.Add(fruits);

            pacman = new PacmanGameObject();
            pacmans.Add(pacman);
            listOfAllGameObjects.Add(pacmans);
            listOfAllGameObjects.Add(monsters);

            List<GameObject> other = new List<GameObject>();
            other.Add(toolBarGameObject = new ToolBarGameObject());
            listOfAllGameObjects.Add(other);

            /*
            List<GameObject> monsterHouses = new List<GameObject>();
            monsterHouses.Add(monsterHouse);
            listOfAllGameObjects.Add(monsterHouses);
            */

            collisionManager = CollisionManager.getInstance();
            collisionManager.Initialize(walls, portals, monsterHouses,
                                        dots, pills, fruits,
                                        pacmans, monsters);




            GameObject.LoadStaticContent();

            foreach(List<GameObject> list in listOfAllGameObjects)
                foreach (GameObject gameObject in list)
                {
                    gameObject.LoadContent();
                }



        }

        public void UnloadContent()
        {
            foreach (List<GameObject> list in listOfAllGameObjects)
                foreach (GameObject gameObject in list)
                {
                    gameObject.UnloadContent();
                }



        }


        #endregion

        #region GameManagement

        internal bool isLevelCompleted()
        {
            return levelCompleted;
        }

        internal bool isGameOver()
        {
            return GameOver;
        }

        #endregion

        #region Update and Draw

        public void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            foreach (List<GameObject> list in listOfAllGameObjects)
                foreach (GameObject gameObject in list)
                {
                    gameObject.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
                }

            foreach (PacmanGameObject pm in pacmans)
            {
                if (pm.IsDead)
                    GameOver = true;
            }

            if (DotGameObject.VisibleDotCounter == 0)
                levelCompleted = true;
        }

        public void Draw(GameTime gameTime)
        {
            foreach (List<GameObject> list in listOfAllGameObjects)
                foreach (GameObject gameObject in list)
                {
                    gameObject.Draw(gameTime);
                }        

        
        } 

        #endregion

        #region Handle Input

        internal void HandleInput(KeyboardState keyboardState)
        {
            foreach (List<GameObject> list in listOfAllGameObjects)
            {
                foreach (GameObject gameObject in list)
                {
                    if(gameObject is MoveableGameObject)
                    {
                        gameObject.HandleInput(keyboardState);
                    }
                }
            }

        }

        #endregion 

    }
}
