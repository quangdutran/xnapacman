using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Pacman.GameManager;
using GameStateManagement;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Pacman.GameObjects
{
    abstract class GameObject :GameScreen
    {
        #region Fields

        protected const int OBJECT_SIZE = 24;
        
        //TODO: move to another place
        protected const int SCALE = 2;

        protected GameCoordinates gamePosition;
        protected Vector2 screenPosition;
        
        protected Texture2D sprite;
        ContentManager content;

        //TODO: to delete
        protected static Random random = new Random();

        #endregion

        #region Initialization

        public override void LoadContent()
        {
            //TODO: check if here is use proper content

            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            sprite = content.Load<Texture2D>("sprite");
        }

        public override void UnloadContent()
        {
        //TODO: add unloading sprite

        }

        #endregion

        #region Update and Draw

        public abstract override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen);
        public abstract override void HandleInput(InputState input);
        public abstract override void Draw(GameTime gameTime);

        #endregion

        
    }
}
