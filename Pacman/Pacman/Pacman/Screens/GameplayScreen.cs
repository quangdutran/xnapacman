#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pacman.GameObjects;
using System.Collections.Generic;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields

        ContentManager content;

        public ContentManager Content
        {
            get { return content; }
            set { content = value; }
        }
        SpriteFont gameFont;

        Vector2 playerPosition = new Vector2(100, 100);
        Vector2 enemyPosition = new Vector2(100, 100);
        List<int> code = new List<int>();

        Random random = new Random();

        GameObjectManager gameObjectManager;

        //TODO: check is it nececcary
//        static GameplayScreen instance = null;

        #endregion

        #region Fields2
        Texture2D sprite;
        int lastMove;
        #endregion


        #region Initialization


     /*   public static GameplayScreen GetInstance()
        {
            if (instance == null)
                instance = new GameplayScreen();
            return instance;
        }
      */ 

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            code.Add(0); //any value is ok.
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
            {
                content = new ContentManager(ScreenManager.Game.Services, "Content");
                
            }
            gameObjectManager = new GameObjectManager();
            gameObjectManager.ScreenManager = ScreenManager;


            //TODO: remove redundant code
            gameObjectManager.ContentManager = content;
            gameObjectManager.SpriteBatch = ScreenManager.SpriteBatch;

            gameObjectManager.LoadContent();


            gameFont = content.Load<SpriteFont>("gamefont");

            sprite = content.Load<Texture2D>("sprite");


            // A real game would probably have more content than this sample, so
            // it would take longer to load. We simulate that by delaying for a
            // while, giving you a chance to admire the beautiful loading screen.
            //Thread.Sleep(1000);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            gameObjectManager.UnloadContent();

            gameObjectManager.ScreenManager = null;
            gameObjectManager.ContentManager = null;
            gameObjectManager.SpriteBatch = null;

            content.Unload();


        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (IsActive)
            {
                // Apply some random jitter to make the enemy move around.
                const float randomization = 10;

                enemyPosition.X += (float)(random.NextDouble() - 0.5) * randomization;
                enemyPosition.Y += (float)(random.NextDouble() - 0.5) * randomization;

                // Apply a stabilizing force to stop the enemy moving off the screen.
                Vector2 targetPosition = new Vector2(200, 200);

                enemyPosition = Vector2.Lerp(enemyPosition, targetPosition, 0.05f);

                // TODO: this game isn't very fun! You could probably improve
                // it by inserting something more interesting in this space :-)

                gameObjectManager.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (gameObjectManager.isLevelCompleted())
            {
                ScreenManager.AddScreen(new LevelCompleteScreen(), ControllingPlayer);
            }
            else if(gameObjectManager.isGameOver())
            {
                ScreenManager.AddScreen(new GameOverScreen(), ControllingPlayer);
            }
            else if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
                // Otherwise move the player position.
                Vector2 movement = Vector2.Zero;

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    movement.X--;
                    lastMove = 1;
                }

                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    movement.X++;
                    lastMove = 3;
                }

                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    movement.Y--;
                    lastMove = 0;
                }

                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    movement.Y++;
                    lastMove = 2;
                }

                #region codes

                if (keyboardState.IsKeyDown(Keys.D0))
                {
                    if (code.Count == 4)
                        code.RemoveAt(0);
                    if(code[code.Count-1] != 0)
                        code.Add(0);
                }

                if (keyboardState.IsKeyDown(Keys.D1))
                {
                    if (code.Count == 4)
                        code.RemoveAt(0);
                    if (code[code.Count - 1] != 1)
                        code.Add(1);
                }

                if (keyboardState.IsKeyDown(Keys.D2))
                {
                    if (code.Count == 4)
                        code.RemoveAt(0);
                    if (code[code.Count - 1] != 2)
                        code.Add(2);
                }

                if (keyboardState.IsKeyDown(Keys.D3))
                {
                    if (code.Count == 4)
                        code.RemoveAt(0);
                    if (code[code.Count - 1] != 3)
                        code.Add(3);
                }

                if (keyboardState.IsKeyDown(Keys.D4))
                {
                    if (code.Count == 4)
                        code.RemoveAt(0);
                    if (code[code.Count - 1] != 4)
                        code.Add(4);
                }

                if (keyboardState.IsKeyDown(Keys.D5))
                {
                    if (code.Count == 4)
                        code.RemoveAt(0);
                    if (code[code.Count - 1] != 5)
                        code.Add(5);
                }

                if (keyboardState.IsKeyDown(Keys.D6))
                {
                    if (code.Count == 4)
                        code.RemoveAt(0);
                    if (code[code.Count - 1] != 6)
                        code.Add(6);
                }

                if (keyboardState.IsKeyDown(Keys.D7))
                {
                    if (code.Count == 4)
                        code.RemoveAt(0);
                    if (code[code.Count - 1] != 7)
                        code.Add(7);
                }

                if (keyboardState.IsKeyDown(Keys.D8))
                {
                    if (code.Count == 4)
                        code.RemoveAt(0);
                    if (code[code.Count - 1] != 8)
                        code.Add(8);
                }

                if (keyboardState.IsKeyDown(Keys.D9))
                {
                    if (code.Count == 4)
                        code.RemoveAt(0);
                    if (code[code.Count - 1] != 9)
                    code.Add(9);
                }

                string strCode = string.Empty;

                foreach(int i in code)
                    strCode += i.ToString();

                switch (strCode)
                { 
                    case "4242":
                        ScreenManager.AddScreen(new LevelCompleteScreen(), ControllingPlayer);
                        break;
                    case "1290":
                        ScreenManager.AddScreen(new GameOverScreen(), ControllingPlayer);
                        break;

                    case "1212":
                        MonsterGameObject.Ghost = true;
                        break;

                }

                #endregion 

                Vector2 thumbstick = gamePadState.ThumbSticks.Left;

                movement.X += thumbstick.X;
                movement.Y -= thumbstick.Y;

                if (movement.Length() > 1)
                    movement.Normalize();

                playerPosition += movement * 2;

                gameObjectManager.HandleInput(keyboardState);
            }

            
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            //ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
              //                                 /*Color.CornflowerBlue*/
                //                               Color.Black, 0, 0);

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            //spriteBatch.DrawString(gameFont, "// TODO", playerPosition, Color.Green);
            /*
            Color color = new Color(255, 255, 255, 255);


            int x = (int)playerPosition.X;

            int i = (x % 20) / 4;

            Rectangle source = new Rectangle((4-i)*24, (lastMove*24), 24, 24);

            Rectangle monsterRectangle = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 48, 48);

            spriteBatch.Draw(sprite, monsterRectangle, source, color);
            */
            gameObjectManager.Draw(gameTime);

            /*
            for (int i = 0; i <= 24; i++)
            {
                for (int j = 0; j <= 18; j++)
                {
                    spriteBatch.Draw(sprite, new Vector2(i * 24, j * 24), new Rectangle(450, 40, 1, 1), new Color(255, 255, 255, 255));
                }
            }
            */
            
            //spriteBatch.DrawString(gameFont, "Insert Gameplay Here", enemyPosition, Color.DarkRed);

            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0)
                ScreenManager.FadeBackBufferToBlack(255 - TransitionAlpha);
        }


        #endregion
    }
}
