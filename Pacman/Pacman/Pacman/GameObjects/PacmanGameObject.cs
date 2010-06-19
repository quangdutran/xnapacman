using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameStateManagement;
using Microsoft.Xna.Framework.Input;

namespace Pacman.GameObjects
{
    class PacmanGameObject:GameObject
    {


        int lastMove;
        const int FRAME_COUNT = 3;

        public override void LoadContent()
        {
            this.source = new Rectangle(5*OBJECT_SIZE, 4*OBJECT_SIZE, OBJECT_SIZE, OBJECT_SIZE);
        }


        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            screenPosition = new Vector2(0.0f + random.Next(5) * 100, 0.0f + random.Next(5) * 100);
        }



        public override void HandleInput(KeyboardState keyboardState)
        {

                // Otherwise move the player position.
                Vector2 movement = Vector2.Zero;

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    movement.X--;
                    lastMove = 2;
                }

                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    movement.X++;
                    lastMove = 0;
                }

                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    movement.Y--;
                    lastMove = 3;
                }

                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    movement.Y++;
                    lastMove = 1;
                }

                if (movement.Length() > 1)
                    movement.Normalize();

                this.screenPosition += movement * 2;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Color color = new Color(255, 255, 255, 255);

            int x = (int)screenPosition.X;

            int i = (x % (20*SCALE)) / (4*SCALE);

            Rectangle animateSource = new Rectangle(((2 - Math.Abs((i - 2))) * OBJECT_SIZE) + 
                                                    (FRAME_COUNT*OBJECT_SIZE*lastMove) + source.X, 
                                                    source.Y,
                                                    source.Width, 
                                                    source.Height);




            Rectangle pacmanRectangle = new Rectangle((int)screenPosition.X, (int)screenPosition.Y, SCALE * OBJECT_SIZE, SCALE * OBJECT_SIZE);

            GameObject.SpriteBatch.Draw(sprite, pacmanRectangle, animateSource, color);
        }
    }
}
