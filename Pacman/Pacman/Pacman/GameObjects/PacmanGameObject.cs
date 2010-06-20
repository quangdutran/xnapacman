using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameStateManagement;
using Microsoft.Xna.Framework.Input;
using Pacman.GameManager;

namespace Pacman.GameObjects
{
    class PacmanGameObject:GameObject
    {

        #region Enums

        public enum direction
        {
            Right = 0,
            Down = 1,
            Left = 2,
            Up = 3
        }

        #endregion
        
        #region Fields

        direction lastMove;
        direction movementDirection;
        const int FRAME_COUNT = 3;

        const int TOLERANCE = 2;
        GameTime lastUpdateTime;
        int updateDelta;
        int drawDelta;

        Vector2 lastDrawPosition;

        #endregion

        #region Properties

        public Vector2 ScreenPosition
        {
            get { return screenPosition; }
        }

        #endregion

        #region Initialization
        
        public PacmanGameObject()
        {
            screenPosition = (new GameCoordinates(0,0)).GetScreenPosition();
            lastDrawPosition = new Vector2();
        }

        public override void LoadContent()
        {
            this.source = new Rectangle(5*OBJECT_SIZE, 4*OBJECT_SIZE, OBJECT_SIZE, OBJECT_SIZE);
        }

        public override void UnloadContent()
        {
            //TODO: add unloading!!
        }


        #endregion

        #region Update, HandleInput and Draw

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            updateDelta += gameTime.ElapsedGameTime.Milliseconds;

            //TODO: remove magic number
            int step = 3;
            
            Vector2 move = Vector2.Zero;

            move = CalculateMove(lastMove, updateDelta, step);
     
            /*
            if (move.Length() > 1)
                move.Normalize();
            */

            //calculate how far coordinate is from Path and compere it with Torerance
            int moduloX = ((int)(screenPosition.X + move.X)) % GameObject.OBJECT_SIZE;
            bool onXPath = GameObject.OBJECT_SIZE / 2 - Math.Abs(moduloX - GameObject.OBJECT_SIZE / 2) <= TOLERANCE;

            int moduloY = ((int)(screenPosition.Y + move.Y)) % GameObject.OBJECT_SIZE;
            bool onYPath = GameObject.OBJECT_SIZE / 2 - Math.Abs(moduloY - GameObject.OBJECT_SIZE / 2) <= TOLERANCE;

            bool moved = false;


            //changing move direction
            if (((onXPath && WasNotOnXPath()) || onYPath && WasNotOnYPath()) &&

                //sprawdzanie kolizji powinno być już wg. poprawionych koordynatów.!
                !CollisionManager.getInstance().IsCollision(this,lastMove,move,GameObjectType.WALLS))
            {
                this.screenPosition += new Vector2((int)move.X, (int)move.Y);
                movementDirection = lastMove;
                moved = true;
            }
            else if (!CollisionManager.getInstance().IsCollision(this, 
                    movementDirection, move = CalculateMove(movementDirection, updateDelta, step), 
                    GameObjectType.WALLS))
            {
            
                this.screenPosition += new Vector2((int)move.X,(int)move.Y);
                moved = true;
            }


            if (moved && onXPath && (lastMove != direction.Right && lastMove != direction.Left)) //if it on pathX, then should be exactly on this path
            {
                //round to neerest 24*X
                float roundX = (((int)this.ScreenPosition.X) % GameObject.OBJECT_SIZE) / (float)GameObject.OBJECT_SIZE;
                this.screenPosition.X = (((int)this.ScreenPosition.X) / GameObject.OBJECT_SIZE) * (float)GameObject.OBJECT_SIZE;
                this.screenPosition.X += (int)(Math.Round(roundX) * OBJECT_SIZE);
            }

            if (moved && onYPath && (lastMove != direction.Up && lastMove != direction.Down)) //if it on pathY, then should be exactly on this path
            {
                //round to neerest 24*Y
                float roundY = (((int)this.ScreenPosition.Y) % GameObject.OBJECT_SIZE) / (float)GameObject.OBJECT_SIZE;
                this.screenPosition.Y = (((int)this.ScreenPosition.Y) / GameObject.OBJECT_SIZE) * (float)GameObject.OBJECT_SIZE;
                this.screenPosition.Y += (int)(Math.Round(roundY) * OBJECT_SIZE);
            }


            updateDelta = 0;
            

        }



        private bool WasNotOnXPath()
        {
            return !(movementDirection == direction.Down || movementDirection == direction.Up);
        }

        private bool WasNotOnYPath()
        {
            return !(movementDirection == direction.Right || movementDirection == direction.Left);
        }

        private Vector2 CalculateMove(direction direction, int updateDelta, int step )
        {
            Vector2 move = Vector2.Zero;
            switch (direction)
            {
                case direction.Up:
                    move = new Vector2(0.0f, -step * 24 * ((float)updateDelta) / 1000);
                    break;

                case direction.Down:
                    move = new Vector2(0.0f, step * 24 * ((float)updateDelta) / 1000);
                    break;

                case direction.Left:
                    move = new Vector2(-step * 24 * (((float)updateDelta) / 1000), 0.0f);
                    break;

                case direction.Right:
                    move = new Vector2(step * 24 * (((float)updateDelta) / 1000), 0.0f);
                    break; 
            }

            return move;
        }


        public override void HandleInput(KeyboardState keyboardState)
        {
            
                // Otherwise move the player position.
                Vector2 movement = Vector2.Zero;

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    movement.X--;
                    lastMove = direction.Left;
                }

                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    movement.X++;
                    lastMove = direction.Right;
                }

                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    movement.Y--;
                    lastMove = direction.Up;
                }

                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    movement.Y++;
                    lastMove = direction.Down;
                }

                if (movement.Length() > 1)
                    movement.Normalize();
            
                //this.screenPosition += movement ;
        }

        public override void Draw(GameTime gameTime)
        {
                int x = (int)screenPosition.X;

                int i = (x % (20 * SCALE)) / (4 * SCALE);

                Rectangle animateSource = new Rectangle(((2 - Math.Abs((i - 2))) * OBJECT_SIZE) +
                                                        (FRAME_COUNT * OBJECT_SIZE * (int)movementDirection) + source.X,
                                                        source.Y,
                                                        source.Width,
                                                        source.Height);

                Rectangle pacmanRectangle = new Rectangle((int)screenPosition.X, (int)screenPosition.Y, SCALE * OBJECT_SIZE, SCALE * OBJECT_SIZE);

                GameObject.SpriteBatch.Draw(sprite, pacmanRectangle, animateSource, Color.White);
        }

        #endregion
    }
}
