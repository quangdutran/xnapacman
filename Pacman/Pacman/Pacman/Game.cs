#region File Description
//-----------------------------------------------------------------------------
// Game.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// Sample showing how to manage different game states, with transitions
    /// between menu screens, a loading screen, the game itself, and a pause
    /// menu. This main game class is extremely simple: all the interesting
    /// stuff happens in the ScreenManager component.
    /// </summary>
    public class GameStateManagementGame : Microsoft.Xna.Framework.Game
    {
        #region Fields

        private GraphicsDeviceManager graphics;
        ScreenManager screenManager;

        private static GameStateManagementGame instance = null;


        public static bool FullScreen
        {
            get {
                if (instance == null)
                {
                    throw new Exception("GameStateManagementGame is not initialized!");
                }
                   
                return instance.graphics.IsFullScreen;
            }

            set {
                if (value != instance.graphics.IsFullScreen)
                    instance.graphics.ToggleFullScreen();
            }
        }

        #endregion

        #region Initialization


        /// <summary>
        /// The main game constructor.
        /// </summary>
        public GameStateManagementGame()
        {
            instance = this;

            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);

            //graphics.PreferredBackBufferWidth = 853;
            //graphics.PreferredBackBufferHeight = 480;

            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
            //graphics.IsFullScreen = true;

            // Create the screen manager component.
            screenManager = new ScreenManager(this);

            Components.Add(screenManager);

            // Activate the first screens.
            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
        }




        #endregion

        #region Draw

        int updateDelta = 0;

        protected override void Update(GameTime gameTime)
        {
            updateDelta += gameTime.ElapsedRealTime.Milliseconds;
            //if (updateDelta > 15)
            {
                base.Update(gameTime);
                updateDelta = 0;
            }
        }


        int drawDelta = 0;

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            drawDelta += gameTime.ElapsedRealTime.Milliseconds;

            if (drawDelta > 30)
            {
                graphics.GraphicsDevice.Clear(Color.Black);

                // The real drawing happens inside the screen manager component.
                base.Draw(gameTime);
            }
        }




        #endregion
    }


    #region Entry Point

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static class Program
    {
        static void Main()
        {
            using (GameStateManagementGame game = new GameStateManagementGame())
            {
                game.Run();
            }
        }
    }

    #endregion
}
