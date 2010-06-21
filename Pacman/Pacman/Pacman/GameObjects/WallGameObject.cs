using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.GameManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman.GameObjects
{
    abstract class WallGameObject:GameObject
    {

        #region Constants

        protected const int LINE_TICKNESS = 2;

        #endregion 

        #region Fields

        protected static Rectangle topLeft, topRight;
        protected static Rectangle bottomLeft, bottomRight;

        protected static bool contentLoaded = false;

        #endregion

        #region Properties

        //GameCoordinates base./*start*/gamePosition;


        //TODO: set propert accessor modifiers
        public GameCoordinates Start
        {
            /*public*/ get { return gamePosition; }
            /*private*/ internal set { 
                /*start*/gamePosition = value;

                screenVectorPosition.X = /*start*/gamePosition.X * OBJECT_SIZE;
                screenVectorPosition.Y = /*start*/gamePosition.Y * OBJECT_SIZE;
            }
        }

        GameCoordinates endGamePosition;

        //TODO: set propert accessor modifiers
        public GameCoordinates End
        {
            /*public*/ get { return endGamePosition; }
            /*private*/ internal set
            {
                endGamePosition = value;

                screenVectorPosition.X = endGamePosition.X * OBJECT_SIZE;
                screenVectorPosition.Y = endGamePosition.Y * OBJECT_SIZE;
            }
        }

        //Vector2 base./*start*/ScreenPosition;

        //for public usage, please see GameCoordinates.GetScreenPosition()
        private Vector2 ScreenPosition
        {
            get { return /*start*/screenVectorPosition; }
        }

        Vector2 endScreenPosition;

        public Vector2 EndScreenPosition
        {
            get { return endScreenPosition; }
        }

        public abstract int Length();
        
        #endregion

        #region Initialization

        public override void LoadContent()
        {
            bottomLeft = new Rectangle(20 * OBJECT_SIZE - LINE_TICKNESS, 6 * OBJECT_SIZE, LINE_TICKNESS, LINE_TICKNESS);
            bottomRight = new Rectangle(19 * OBJECT_SIZE, 6 * OBJECT_SIZE, LINE_TICKNESS, LINE_TICKNESS);

            topLeft = new Rectangle(20 * OBJECT_SIZE - LINE_TICKNESS, 7 * OBJECT_SIZE - LINE_TICKNESS, LINE_TICKNESS, LINE_TICKNESS);
            topRight = new Rectangle(19 * OBJECT_SIZE, 7 * OBJECT_SIZE - LINE_TICKNESS, LINE_TICKNESS, LINE_TICKNESS);

            contentLoaded = true;

        }

        #endregion 

        #region Dummy update

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen){}

        #endregion

    }


    class VerticalWallGameObject : WallGameObject
    {


        #region Fields

        Rectangle leftSide;
        Rectangle rightSide;

        #endregion

        #region Initialization

        public VerticalWallGameObject(int x, int startY, int endY)
        {
            Start = new GameCoordinates(x, startY);
            End = new GameCoordinates(x, endY);
        }

        public override void LoadContent()
        {
            if (!WallGameObject.contentLoaded)
                base.LoadContent();
            
            leftSide  = new Rectangle(20 * OBJECT_SIZE - LINE_TICKNESS, 5 * OBJECT_SIZE, LINE_TICKNESS, OBJECT_SIZE);
            rightSide = new Rectangle(19 * OBJECT_SIZE, 5 * OBJECT_SIZE, LINE_TICKNESS, OBJECT_SIZE);

        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            DrawBegining(Start);

            for(int y=Start.Y; y < End.Y; y++)
                DrawLine(new GameCoordinates(Start.X, y));

            DrawEnd(End);
        }

        private void DrawLine(GameCoordinates gameCoordinates)
        {
            Vector2 position = gameCoordinates.GetScreenPosition();

            Color color = new Color(255, 255, 255, 255);

            GameObject.SpriteBatch.Draw(sprite, 
                new Rectangle((int)(position.X-(LINE_TICKNESS*SCALE)), (int)position.Y, LINE_TICKNESS*SCALE, OBJECT_SIZE*SCALE), 
                leftSide, 
                color);
            
            GameObject.SpriteBatch.Draw(sprite, 
                new Rectangle((int)(position.X),(int)position.Y, LINE_TICKNESS*SCALE, OBJECT_SIZE*SCALE), 
                rightSide, 
                color);
            
        }

        private void DrawBegining(GameCoordinates gameCoordinates)
        {
            Vector2 position = gameCoordinates.GetScreenPosition();

            Color color = new Color(255, 255, 255, 255);

            //topleft
            GameObject.SpriteBatch.Draw(sprite,
                new Rectangle((int)(position.X - LINE_TICKNESS * SCALE),
                              (int)(position.Y - LINE_TICKNESS * SCALE),
                              LINE_TICKNESS * SCALE,
                              LINE_TICKNESS * SCALE),
                topLeft,
                color);


            //topRight
            GameObject.SpriteBatch.Draw(sprite,
                new Rectangle((int)(position.X),
                              (int)(position.Y - LINE_TICKNESS * SCALE),
                              LINE_TICKNESS * SCALE,
                              LINE_TICKNESS * SCALE),
                topRight,
                color);


        }

        private void DrawEnd(GameCoordinates gameCoordinates)
        {
            Vector2 position = gameCoordinates.GetScreenPosition();

            Color color = new Color(255, 255, 255, 255);

            //bottomleft
            GameObject.SpriteBatch.Draw(sprite,
                new Rectangle((int)(position.X - LINE_TICKNESS*SCALE),
                              (int)(position.Y),
                              LINE_TICKNESS * SCALE,
                              LINE_TICKNESS * SCALE),
                bottomLeft,
                color);


            //bottomRight
            GameObject.SpriteBatch.Draw(sprite,
                new Rectangle((int)(position.X),
                              (int)(position.Y),
                              LINE_TICKNESS * SCALE,
                              LINE_TICKNESS * SCALE),
                bottomRight,
                color);


        }

        #endregion

        #region Other

        public override int Length()
        {
            return End.Y - Start.Y;
        }

        #endregion
    }


    class HorizontalWallGameObject : WallGameObject
    {

        #region Fields

        Rectangle top;
        Rectangle bottom;

        #endregion

        #region Initialization


        public HorizontalWallGameObject(int y, int startX, int endX)
        {
            Start = new GameCoordinates(startX, y);
            End = new GameCoordinates(endX, y);
        }

        public override void LoadContent()
        {
            if (!WallGameObject.contentLoaded)
                base.LoadContent();

            top = new Rectangle(19 * OBJECT_SIZE, 5 * OBJECT_SIZE - LINE_TICKNESS * SCALE, OBJECT_SIZE, LINE_TICKNESS);
            bottom = new Rectangle(19 * OBJECT_SIZE, 4 * OBJECT_SIZE, OBJECT_SIZE, LINE_TICKNESS);

        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            DrawBegining(Start);

            for (int x = Start.X; x < End.X; x++)
                DrawLine(new GameCoordinates(x, Start.Y));

            DrawEnd(End);
        }

        private void DrawLine(GameCoordinates gameCoordinates)
        {
            Vector2 position = gameCoordinates.GetScreenPosition();

            Color color = new Color(255, 255, 255, 255);

            GameObject.SpriteBatch.Draw(sprite,
                new Rectangle((int)(position.X), (int)position.Y-(LINE_TICKNESS * SCALE), OBJECT_SIZE * SCALE, LINE_TICKNESS * SCALE),
                top,
                color);

            GameObject.SpriteBatch.Draw(sprite,
                new Rectangle((int)(position.X), (int)position.Y, OBJECT_SIZE * SCALE, LINE_TICKNESS * SCALE),
                bottom,
                color);

        }

        private void DrawBegining(GameCoordinates gameCoordinates)
        {
            Vector2 position = gameCoordinates.GetScreenPosition();

            Color color = new Color(255, 255, 255, 255);

            //topleft
            GameObject.SpriteBatch.Draw(sprite,
                new Rectangle((int)(position.X - LINE_TICKNESS * SCALE),
                              (int)(position.Y - LINE_TICKNESS * SCALE),
                              LINE_TICKNESS * SCALE,
                              LINE_TICKNESS * SCALE),
                topLeft,
                color);

            //bottomleft
            GameObject.SpriteBatch.Draw(sprite,
                new Rectangle((int)(position.X - LINE_TICKNESS * SCALE),
                              (int)(position.Y),
                              LINE_TICKNESS * SCALE,
                              LINE_TICKNESS * SCALE),
                bottomLeft,
                color);

            
        }

        private void DrawEnd(GameCoordinates gameCoordinates)
        {
            Vector2 position = gameCoordinates.GetScreenPosition();

            Color color = new Color(255, 255, 255, 255);

            //topRight
            GameObject.SpriteBatch.Draw(sprite,
                new Rectangle((int)(position.X),
                              (int)(position.Y - LINE_TICKNESS * SCALE),
                              LINE_TICKNESS * SCALE,
                              LINE_TICKNESS * SCALE),
                topRight,
                color);


            //bottomRight
            GameObject.SpriteBatch.Draw(sprite,
                new Rectangle((int)(position.X),
                              (int)(position.Y),
                              LINE_TICKNESS * SCALE,
                              LINE_TICKNESS * SCALE),
                bottomRight,
                color);
        }

        #endregion

        #region Other

        public override int Length()
        {
            return End.X - Start.X;
        }
        
        #endregion
    }
}
