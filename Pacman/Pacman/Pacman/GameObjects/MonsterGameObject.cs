using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameStateManagement;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pacman.GameManager;
using System.Diagnostics;

namespace Pacman.GameObjects
{
    class MonsterGameObject: MoveableGameObject
    {

        #region Fields

        static bool ghost = false;

        static bool ghostTimeGot = true;
        static double ghostTimeStart = 0.0;
        static int ghostMaxTime = 15;
        const int WARNING_GHOST_TIME = 5;
        static bool blinking = false;


        bool dead = false;

        public bool IsDead
        {
            get { return dead; }
        }

        bool deadTimeGot = true;
        double deadTime = 0.0;
        int deadMaxTime = 15;


        GameCoordinates gameCoordinatesOfNearestPacman = new GameCoordinates();
        
        bool fakePacmanPositionSet = false;
        Rectangle ghostSource;

        
        static float STEP = MoveableGameObject.STEP;

        public static bool Ghost
        {
            get { return MonsterGameObject.ghost; }
            set {
                if (MonsterGameObject.ghost != value)
                {
                    if (value == true)
                    {
                        ghostTimeGot = false;
                        MonsterGameObject.ghost = value;
                        blinking = false;
                        STEP = 3.0f;
                    }
                    else
                    {
                        //ghostMaxTime -= 3;
                        STEP = MoveableGameObject.STEP;
                        MonsterGameObject.ghost = value;
                    }
                }
            }
        }

        public enum MonsterGameObjectColor
        { 
          Blue,
          Green,
          Pink,
          Red
        }



        MonsterGameObjectColor monsterColor;
        direction[] nextMoveAlternative = new direction[3];
        int nextMoveAlternativeNumber = 0;

        #endregion

        #region Initialization

        public MonsterGameObject(MonsterGameObjectColor color)
        {
            monsterColor = color;
            //TODO: remove magic numbers
            screenVectorPosition = (new GameCoordinates(random.Next(24), random.Next(18))).GetScreenPosition();

            startLastMovePosition = GameVectorPosition;
            Ghost = false;
        }


        public override void LoadContent()
        {
             switch (monsterColor)
            { 
                case MonsterGameObjectColor.Blue:
                    this.source = new Rectangle(0, 0, OBJECT_SIZE, OBJECT_SIZE);
                break;

                case MonsterGameObjectColor.Green:
                    this.source = new Rectangle(5*OBJECT_SIZE, 0, OBJECT_SIZE, OBJECT_SIZE);
                break;

                case MonsterGameObjectColor.Pink:
                    this.source = new Rectangle(10*OBJECT_SIZE, 0, OBJECT_SIZE, OBJECT_SIZE);    
                break;

                case MonsterGameObjectColor.Red:
                    this.source = new Rectangle(15*OBJECT_SIZE, 0, OBJECT_SIZE, OBJECT_SIZE);
                break;

            }

             ghostSource = new Rectangle(0 * OBJECT_SIZE, 4 * OBJECT_SIZE, OBJECT_SIZE, OBJECT_SIZE);

        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if(!ghostTimeGot)
            {
                ghostTimeStart = gameTime.TotalGameTime.TotalSeconds;
                ghostTimeGot = true;
            }

            if (ghost && gameTime.TotalGameTime.TotalSeconds - ghostTimeStart > ghostMaxTime)
            {
                Ghost = false;
                fakePacmanPositionSet = false;
                blinking = false;
            }

            if (ghost && gameTime.TotalGameTime.TotalSeconds - ghostTimeStart > (ghostMaxTime-WARNING_GHOST_TIME))
            {
              blinking = true; 
            
            }

            if (!deadTimeGot)
            {
                deadTime = gameTime.TotalGameTime.TotalSeconds;
                deadTimeGot = true;
            }

            if(dead && !ghost && gameTime.TotalGameTime.TotalSeconds - deadTime > deadMaxTime)
            {
                dead = false;
                screenVectorPosition = (new GameCoordinates(random.Next(24), random.Next(18))).GetScreenPosition();
            }

        }

        #endregion 

        #region HandleInput and Draw

        public override void HandleInput(KeyboardState keyboardState)
        {
            if (dead)
            { 
            
            }
            if (true)
            {
                List<GameCoordinates> pacmansPositions = PacmanGameObject.PacmansPositions;

                float min = float.MaxValue;

                
                if (ghost)
                {
                    if (!fakePacmanPositionSet)
                    {
                        gameCoordinatesOfNearestPacman = new GameCoordinates(random.Next(23), random.Next(17));
                        fakePacmanPositionSet = true;
                    }
                }
                else
                {
                    gameCoordinatesOfNearestPacman = pacmansPositions[0];
                    foreach (GameCoordinates gc in pacmansPositions)
                    {
                        float tmp = Vector2.Distance(new Vector2(gc.X, gc.Y), new Vector2(GameVectorPosition.X, GameVectorPosition.Y));
                        if (tmp < min)
                        {
                            min = tmp;
                            gameCoordinatesOfNearestPacman = gc;
                        }
                    }
                }
                bool equal = Math.Abs(gameCoordinatesOfNearestPacman.X - GameVectorPosition.X) ==
                    Math.Abs(gameCoordinatesOfNearestPacman.Y - GameVectorPosition.Y);

                if ((equal && random.Next(2) == 0) ||
                    (Math.Abs(gameCoordinatesOfNearestPacman.X - GameVectorPosition.X) >
                     Math.Abs(gameCoordinatesOfNearestPacman.Y - GameVectorPosition.Y)))
                {//closer by X

                    nextMove = (gameCoordinatesOfNearestPacman.X > GameVectorPosition.X) ? direction.Right : direction.Left;
                    nextMoveAlternative[0] = (gameCoordinatesOfNearestPacman.Y > GameVectorPosition.Y) ? direction.Down : direction.Up;
                    nextMoveAlternative[1] = (gameCoordinatesOfNearestPacman.X > GameVectorPosition.X) ? direction.Left : direction.Right;
                    nextMoveAlternative[2] = (gameCoordinatesOfNearestPacman.Y > GameVectorPosition.Y) ? direction.Up : direction.Down;
                }
                else
                {
                    nextMove = (gameCoordinatesOfNearestPacman.Y > GameVectorPosition.Y) ? direction.Down : direction.Up;
                    nextMoveAlternative[0] = (gameCoordinatesOfNearestPacman.X > GameVectorPosition.X) ? direction.Right : direction.Left;
                    nextMoveAlternative[1] = (gameCoordinatesOfNearestPacman.Y > GameVectorPosition.Y) ? direction.Up : direction.Down;
                    nextMoveAlternative[2] = (gameCoordinatesOfNearestPacman.X > GameVectorPosition.X) ? direction.Left : direction.Right;
                }
            }
            else
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
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (!dead)
            {
                bool drawGhost = ghost;


                if (blinking)
                {
                    System.Console.WriteLine(gameTime.ElapsedGameTime.Milliseconds.ToString());
                    if (gameTime.TotalGameTime.Milliseconds/4 > 125)
                        drawGhost = true;
                    else
                        drawGhost = false;

                }

                if (drawGhost)
                {
                    int x = (int)screenVectorPosition.X;

                    int i = (x % 20) / 4;

                    Vector2 moveVector = new Vector2((4 - i) * 24,0);


                    Rectangle monsterRectangle = new Rectangle((int)screenVectorPosition.X, (int)screenVectorPosition.Y, SCALE * OBJECT_SIZE, SCALE * OBJECT_SIZE);
                    GameObject.SpriteBatch.Draw(sprite, monsterRectangle,
                                                new Rectangle((int)(ghostSource.X + moveVector.X),
                                                              (int)(ghostSource.Y + moveVector.Y),
                                                              ghostSource.Width,
                                                              ghostSource.Height),
                                                Color.White);
                }
                else
                {
                    int x = (int)screenVectorPosition.X;

                    int i = (x % 20) / 4;

                    Vector2 moveVector = new Vector2((4 - i) * 24, ConvertToImagePosition(movementDirection) * 24);



                    Rectangle monsterRectangle = new Rectangle((int)screenVectorPosition.X, (int)screenVectorPosition.Y, SCALE * OBJECT_SIZE, SCALE * OBJECT_SIZE);
                    GameObject.SpriteBatch.Draw(sprite, monsterRectangle, 
                                                new Rectangle((int)(source.X + moveVector.X),
                                                              (int)(source.Y + moveVector.Y),
                                                              source.Width,
                                                              source.Height),
                                                Color.White);
                }

                
            }
        }

        int ConvertToImagePosition(direction dir)
        {
            switch (dir)
            { 
                case direction.Up:
                    return 0;
                case direction.Left:
                    return 1;
                case direction.Down:
                    return 2;
                case direction.Right:
                    return 3;
            }
            return 0;
        }

        #endregion


        GameCoordinates startLastMovePosition;
        bool moved = false;
        public override bool makeMove()
        {
            bool stillThisSameGamePosition = (startLastMovePosition.X == GameVectorPosition.X && startLastMovePosition.Y == GameVectorPosition.Y);
            bool wantsChangeDirection = nextMove != movementDirection;
            direction lastMoveBackUp = nextMove;

            
            //System.Console.WriteLine("wantsChangeDirection: " + wantsChangeDirection.ToString() + 
            //                       "\nstillThisSameGamePosition: " + stillThisSameGamePosition.ToString());
             

            if (wantsChangeDirection//we want to take a turn 
                && (stillThisSameGamePosition && moved))
            {
                nextMove = movementDirection;
            }
            //else
              //  System.Console.WriteLine("turn from " + lastMoveBackUp.ToString() + " to " +lastMove.ToString());

            Vector2 move = CalculateMove(nextMove, updateDelta, STEP);
            moved = false;

            //calculate how far coordinate is from Path and compere it with Torerance
            int moduloY = ((int)(screenVectorPosition.Y + move.Y)) % GameObject.OBJECT_SIZE;
            bool onHorizontalPath = GameObject.OBJECT_SIZE / 2 - Math.Abs(moduloY - GameObject.OBJECT_SIZE / 2) <= TOLERANCE;

            int moduloX = ((int)(screenVectorPosition.X + move.X)) % GameObject.OBJECT_SIZE;
            bool onVerticalPath = GameObject.OBJECT_SIZE / 2 - Math.Abs(moduloX - GameObject.OBJECT_SIZE / 2) <= TOLERANCE;


            

            //changing move direction
            if (((onVerticalPath && !WasOnVerticalPath()) || onHorizontalPath && !WasOnHorizontalPath()) &&
                !CollisionManager.getInstance().IsCollision(roundRectangleToPath(this.ScreenRectanglePosition, move, nextMove, onHorizontalPath, onVerticalPath), nextMove, GameObjectType.WALLS))
            {
                this.screenVectorPosition += new Vector2((int)move.X, (int)move.Y);
                this.ScreenRectanglePosition = roundRectangleToPath(this.ScreenRectanglePosition, move, movementDirection, onHorizontalPath, onVerticalPath);

                if(movementDirection != nextMove)
                    startLastMovePosition = GameVectorPosition;

                movementDirection = nextMove;
                

                moved = true;

            }
            else if (!CollisionManager.getInstance().IsCollision(roundRectangleToPath(this.ScreenRectanglePosition,
                move = CalculateMove(movementDirection, updateDelta, STEP),
                movementDirection, onHorizontalPath, onVerticalPath), movementDirection, GameObjectType.WALLS))
            {

                this.screenVectorPosition += new Vector2((int)move.X, (int)move.Y);
                this.ScreenRectanglePosition = roundRectangleToPath(this.ScreenRectanglePosition, move, movementDirection, onHorizontalPath, onVerticalPath);
                moved = true;
            }

            if (!moved)
            {
                if (nextMoveAlternativeNumber < 3)
                {
                    nextMove = nextMoveAlternative[nextMoveAlternativeNumber];
                    nextMoveAlternativeNumber++;
                    makeMove();
                }
            }
            else
            {
                if(nextMoveAlternativeNumber!=0)
                System.Console.WriteLine("Move number" + nextMoveAlternativeNumber.ToString());

                nextMoveAlternativeNumber = 0;
            }




            return moved;
        
        }

        internal void Kill()
        {
            if(!dead)
                deadTimeGot = false;
            dead = true;
        }
    }
}
