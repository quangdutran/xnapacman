using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameStateManagement;
using Microsoft.Xna.Framework.Input;
using Pacman.GameManager;
using System.Diagnostics;

namespace Pacman.GameObjects
{
    class PacmanGameObject: MoveableGameObject
    {

        #region Fields

        int points = 0;
        static List<PacmanGameObject> pacmans = null;

        #endregion

        #region Properties

        public static List<GameCoordinates> PacmansPositions
        {
            get {
                List<GameCoordinates> positions = new List<GameCoordinates>();
                foreach(PacmanGameObject p in pacmans)
                    positions.Add(p.GameVectorPosition);

                return positions;
            }
        }

        public bool IsDead
        {
            get { return isDead;}
            private set { isDead = value; }
        } 

        bool isDead;

        #endregion

        #region Initialization

        public PacmanGameObject()
        {
            IsDead = false;
            screenVectorPosition = (new GameCoordinates(0,0)).GetScreenPosition();
        }

        public override void LoadContent()
        {
            if(pacmans == null)
                 pacmans = new List<PacmanGameObject>();

            pacmans.Add(this);

            this.source = new Rectangle(5*OBJECT_SIZE, 4*OBJECT_SIZE, OBJECT_SIZE, OBJECT_SIZE);
        }

        public override void UnloadContent()
        {
            if (pacmans != null)
                pacmans = null;

            //TODO: add unloading!!
        }


        #endregion

        #region Update

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (CollisionManager.getInstance().IsCollision(ScreenRectanglePosition, movementDirection, GameObjectType.DOTS))
                points += 10;


            if(CollisionManager.getInstance().IsCollision(ScreenRectanglePosition,movementDirection,GameObjectType.MONSTERS))
            {
                IsDead = true;
            }

            if (CollisionManager.getInstance().IsCollision(ScreenRectanglePosition, movementDirection, GameObjectType.GHOSTS))
            {
                points += 200;
            }

            if(CollisionManager.getInstance().IsCollision(ScreenRectanglePosition,movementDirection,GameObjectType.PILLS))
            {
                MonsterGameObject.Ghost = true;                
            }


            updateDelta = 0;


        }

        public override bool makeMove()
        {
            Vector2 move = Vector2.Zero;

            move = CalculateMove(nextMove, updateDelta, STEP);

            //calculate how far coordinate is from Path and compere it with Torerance
            int moduloY = ((int)(screenVectorPosition.Y + move.Y)) % GameObject.OBJECT_SIZE;
            bool onHorizontalPath = GameObject.OBJECT_SIZE / 2 - Math.Abs(moduloY - GameObject.OBJECT_SIZE / 2) <= TOLERANCE;

            int moduloX = ((int)(screenVectorPosition.X + move.X)) % GameObject.OBJECT_SIZE;
            bool onVerticalPath = GameObject.OBJECT_SIZE / 2 - Math.Abs(moduloX - GameObject.OBJECT_SIZE / 2) <= TOLERANCE;


            bool moved = false;

            //changing move direction
            if (((onVerticalPath && !WasOnVerticalPath()) || onHorizontalPath && !WasOnHorizontalPath()) &&
                !CollisionManager.getInstance().IsCollision(roundRectangleToPath(this.ScreenRectanglePosition, move, nextMove, onHorizontalPath, onVerticalPath), nextMove, GameObjectType.WALLS))
            {
                this.screenVectorPosition += new Vector2((int)move.X, (int)move.Y);
                movementDirection = nextMove;
                moved = true;
            }
            else if (!CollisionManager.getInstance().IsCollision(roundRectangleToPath(this.ScreenRectanglePosition,
                move = CalculateMove(movementDirection, updateDelta, STEP),
                movementDirection, onHorizontalPath, onVerticalPath), movementDirection, GameObjectType.WALLS))
            {

                this.screenVectorPosition += new Vector2((int)move.X, (int)move.Y);
                moved = true;
            }

            if (moved)
                this.ScreenRectanglePosition = roundRectangleToPath(this.ScreenRectanglePosition, move, movementDirection, onHorizontalPath, onVerticalPath);


            return moved;
        }


        #endregion

        #region HandleInput and Draw

        public override void HandleInput(KeyboardState keyboardState)
        {
            if (true)
            {
                // Otherwise move the player position.
                Vector2 movement = Vector2.Zero;

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    movement.X--;
                    nextMove = direction.Left;
                }

                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    movement.X++;
                    nextMove = direction.Right;
                }

                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    movement.Y--;
                    nextMove = direction.Up;
                }

                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    movement.Y++;
                    nextMove = direction.Down;
                }

                if (movement.Length() > 1)
                    movement.Normalize();

                //this.screenPosition += movement*2 ;
            }
        }

        public override void Draw(GameTime gameTime)
        {
                int x = (int)screenVectorPosition.X;
                int y = (int)screenVectorPosition.Y;

                int i = ((x+y) % (24 * SCALE)) / (6 * SCALE);

                int[] array = new int[]{0, 1, 2, 1};

                int img = array[i]; 

                Rectangle animateSource = new Rectangle(( img * OBJECT_SIZE) +
                                                        (FRAME_COUNT * OBJECT_SIZE * (int)movementDirection) + source.X,
                                                        source.Y,
                                                        source.Width,
                                                        source.Height);

                Rectangle pacmanRectangle = new Rectangle((int)screenVectorPosition.X, (int)screenVectorPosition.Y-2, SCALE * OBJECT_SIZE, SCALE * OBJECT_SIZE);

                GameObject.SpriteBatch.Draw(sprite, pacmanRectangle, animateSource, Color.White);
        }

        #endregion

       
    }
}
