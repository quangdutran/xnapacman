using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Pacman.GameManager;
using Microsoft.Xna.Framework.Input;

namespace Pacman.GameObjects
{
    abstract class MoveableGameObject : GameObject
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

        protected direction nextMove;
        protected direction movementDirection;

        protected const int FRAME_COUNT = 3;
        protected const int TOLERANCE = 2;
        protected const float STEP = 3.0f;

        protected int updateDelta;

        #endregion

        #region Properties

        public Rectangle ScreenRectanglePosition
        {
            get { return new Rectangle((int)screenVectorPosition.X, (int)screenVectorPosition.Y, GameObject.OBJECT_SIZE * GameObject.SCALE, GameObject.OBJECT_SIZE * GameObject.SCALE); }
            set { screenVectorPosition.X = value.X; screenVectorPosition.Y = value.Y; }
        }

        public Vector2 ScreenVectorPosition
        {
            get { return screenVectorPosition; }
        }

        public GameCoordinates GameVectorPosition
        {
            get {
                return (GameCoordinates.ScreenPositionToGameCoortinates(
                    new Vector2(ScreenVectorPosition.X+TOLERANCE, screenVectorPosition.Y+TOLERANCE)));
            }
        }

        #endregion

        #region Update

        

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

            //TODO: remove unnecessary field updateDelta here and in MonstarGameObject
            updateDelta += gameTime.ElapsedGameTime.Milliseconds;


            bool moved = makeMove();



            updateDelta = 0;

        }

        public abstract bool makeMove();
        

        protected bool WasOnVerticalPath()
        {
            return movementDirection == direction.Down || movementDirection == direction.Up;
        }

        protected bool WasOnHorizontalPath()
        {// <------------->
            return movementDirection == direction.Right || movementDirection == direction.Left;
        }

        protected Vector2 CalculateMove(direction direction, int updateDelta, float STEP)
        {
            Vector2 move = Vector2.Zero;
            switch (direction)
            {
                case direction.Up:
                    move = new Vector2(0.0f, -STEP * 24 * ((float)updateDelta) / 1000);
                    break;

                case direction.Down:
                    move = new Vector2(0.0f, STEP * 24 * ((float)updateDelta) / 1000);
                    break;

                case direction.Left:
                    move = new Vector2(-STEP * 24 * (((float)updateDelta) / 1000), 0.0f);
                    break;

                case direction.Right:
                    move = new Vector2(STEP * 24 * (((float)updateDelta) / 1000), 0.0f);
                    break;
            }

            return move;
        }

        protected Rectangle roundRectangleToPath(Rectangle screenRectangle, Vector2 move, direction direction, bool onHorizontalPath, bool onVerticalPath)
        {
            screenRectangle.X += (int)move.X;
            screenRectangle.Y += (int)move.Y;

            if (onVerticalPath && (direction != direction.Right && direction != direction.Left)) //if it on pathX, then should be exactly on this path
            {
                //round to neerest 24*X
                float roundX = (((int)screenRectangle.X) % GameObject.OBJECT_SIZE) / (float)GameObject.OBJECT_SIZE;
                /*base*/
                screenRectangle.X = (((int)screenRectangle.X) / GameObject.OBJECT_SIZE) * GameObject.OBJECT_SIZE;
                screenRectangle.X += (int)(Math.Round(roundX) * OBJECT_SIZE);
            }

            if (onHorizontalPath && (direction != direction.Up && direction != direction.Down)) //if it on pathY, then should be exactly on this path
            {
                //round to neerest 24*Y
                float roundY = (((int)screenRectangle.Y) % GameObject.OBJECT_SIZE) / (float)GameObject.OBJECT_SIZE;
                /*base*/
                screenRectangle.Y = (((int)screenRectangle.Y) / GameObject.OBJECT_SIZE) * GameObject.OBJECT_SIZE;
                screenRectangle.Y += (int)(Math.Round(roundY) * OBJECT_SIZE);
            }


            return screenRectangle;

        }

        #endregion 

        #region HandleInput and Draw

        public abstract override void HandleInput(KeyboardState keyboardState);

        public abstract override void Draw(GameTime gameTime);

        #endregion

    }
}
