﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Pacman.GameManager;
using GameStateManagement;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Pacman.GameObjects
{
    abstract class GameObject
    {
        
        public enum GameObjectType
        {
            WALLS,
            PORTALS,
            MONSTER_HOUSES,
            DOTS,
            PILLS,
            FRUITS,
            PACMANS,
            MONSTERS,
            GHOSTS
        }
        
        #region Fields

        public static int OBJECT_SIZE = 24;
        
        //TODO: move to another place
        public const int SCALE = 1;

        protected GameCoordinates gamePosition;
        protected Vector2 screenVectorPosition;
        protected Rectangle source;
        
        //Static fields
        protected static Texture2D sprite;
        protected static SpriteFont gameFontCourierNew20;
        protected static SpriteFont gameFontCourierNew32;
        protected static SpriteFont gameFontCourierNew40;
        
        //TODO: to delete
        protected static Random random = new Random();

        #endregion

        #region Properties

        //TODO: change namespaces.... Pacman.GameObjects should be inside Pacman.GameManager
        private static ContentManager content;

        /*internal*/ public static ContentManager Content
        {
            get { return GameObject.content; }
            /*internal*/ set { GameObject.content = value; }
        }

        static SpriteBatch spriteBatch;

        /*internal*/ public static SpriteBatch SpriteBatch
        {
            get { return GameObject.spriteBatch; }
            /*internal*/ set { GameObject.spriteBatch = value; }
        }

        #endregion

        #region Initialization

        public static void LoadStaticContent()
        {
            //TODO: check if here is use proper content

        //    if (content == null)
        //        content = new ContentManager(ScreenManager.Game.Services, "Content");

            sprite = content.Load<Texture2D>("sprite");
            gameFontCourierNew20 = content.Load<SpriteFont>("gameFontCourierNew20");
            gameFontCourierNew32 = content.Load<SpriteFont>("gameFontCourierNew32");
            gameFontCourierNew40 = content.Load<SpriteFont>("gameFontCourierNew40");
        }

        public abstract void LoadContent();

        public virtual void UnloadContent()
        {
        //TODO: add unloading sprite

        }

        #endregion

        #region Update and Draw

        public abstract void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen);
        public virtual void HandleInput(KeyboardState keyboardState) { }
        public abstract void Draw(GameTime gameTime);

        #endregion




    }
}
