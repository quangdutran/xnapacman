using System;
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

        #region Fields



        List<GameObject> monsters = new List<GameObject>();
        List<GameObject> dots = new List<GameObject>();
        List<GameObject> walls;
        List<GameObject> portals = new List<GameObject>();
        List<GameObject> fruits = new List<GameObject>();
        List<GameObject> pacmans = new List<GameObject>();

        List<List<GameObject>> listOfAllGameObjects = new List<List<GameObject>>();

        PacmanGameObject pacman;
        MonsterGameObject monsterHouse;

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
        }


        public void LoadContent()
        {

            /*
            monsters.Add(new MonsterGameObject(MonsterGameObject.MonsterGameObjectColor.Blue));
            monsters.Add(new MonsterGameObject(MonsterGameObject.MonsterGameObjectColor.Green));
            monsters.Add(new MonsterGameObject(MonsterGameObject.MonsterGameObjectColor.Pink));
            monsters.Add(new MonsterGameObject(MonsterGameObject.MonsterGameObjectColor.Red));
            */

            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;

            GameCoordinates topLeftArena = new GameCoordinates(0, 0);
            GameCoordinates bottomRightArena = new GameCoordinates();
            
            //TODO:remove magic number
            bottomRightArena.X = viewport.Width / 24 -2;
            bottomRightArena.Y = viewport.Height / 24 -2;

            walls = board.Walls;

            //borders
            
            walls.Add(new HorizontalWallGameObject(topLeftArena.Y, topLeftArena.X, bottomRightArena.X));
            walls.Add(new HorizontalWallGameObject(bottomRightArena.Y, topLeftArena.X, bottomRightArena.X));

            walls.Add(new VerticalWallGameObject(topLeftArena.X, topLeftArena.Y, bottomRightArena.Y));
            walls.Add(new VerticalWallGameObject(bottomRightArena.X, topLeftArena.Y, bottomRightArena.Y));
            



            /*
            walls.Add(new VerticalWallGameObject(2, 3, 8));
            walls.Add(new VerticalWallGameObject(5, 1, 3));

            walls.Add(new HorizontalWallGameObject(1, 1, 3));
            walls.Add(new HorizontalWallGameObject(2, 3, 8));
            */

            listOfAllGameObjects.Add(monsters);
            listOfAllGameObjects.Add(dots);
            listOfAllGameObjects.Add(walls);
            listOfAllGameObjects.Add(portals);
            listOfAllGameObjects.Add(fruits);

            pacman = new PacmanGameObject();
            pacmans.Add(pacman);
            listOfAllGameObjects.Add(pacmans);

            /*
            List<GameObject> monsterHouses = new List<GameObject>();
            monsterHouses.Add(monsterHouse);
            listOfAllGameObjects.Add(monsterHouses);
            */





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

        #region Update and Draw

        public void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        { 
        
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


        internal void HandleInput(KeyboardState keyboardState)
        {
            foreach (GameObject pacman in pacmans)
            {
                pacman.HandleInput(keyboardState);
            }       
        }
    }
}
