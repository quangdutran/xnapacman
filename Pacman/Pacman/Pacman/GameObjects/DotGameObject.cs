using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman.GameManager;

namespace Pacman.GameObjects
{
    class DotGameObject:GameObject
    {


        #region Static Generator and static conent

        static public DotGameObject[,] array;
        static public List<GameObject> Generate(int x, int y)
        {
            visibleDotCounter = 0;

            List<GameObject> list = new List<GameObject>();
            array = new DotGameObject[x,y];

            DotGameObject tmp;
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                {
                    tmp = new DotGameObject(i, j);
                    array[i,j] = tmp;
                    list.Add(tmp);
                }

            return list;

        }

        static int visibleDotCounter = 0;

        public static int VisibleDotCounter
        {
            get { return visibleDotCounter; }
        }

        #endregion

        const int DOT_SIZE = 4;
        bool visible;
        
        #region Properties

        public Rectangle ScreenRectangle
        {
            get
            {
                return new Rectangle((int)screenVectorPosition.X, 
                                     (int)screenVectorPosition.Y,
                                      DOT_SIZE, 
                                      DOT_SIZE);
            }
        }

        #endregion


        #region Initialization



        private DotGameObject(int x, int y)
        {
            screenVectorPosition = (new GameCoordinates(x, y)).GetScreenPosition();
            screenVectorPosition += new Vector2(9, 9);
            visible = true;
            visibleDotCounter++;
        }

        #endregion


        public override void LoadContent()
        {
            this.source = new Rectangle(17 * OBJECT_SIZE+9, 4 * OBJECT_SIZE+9, 6, 6);
        }

        public override void UnloadContent()
        {
            visibleDotCounter = 0;
        }

        #region Draw

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (visible)
            {
                Rectangle rect = new Rectangle((int)screenVectorPosition.X, 
                                               (int)screenVectorPosition.Y,
                                               6, 
                                               6);

                GameObject.SpriteBatch.Draw(sprite, rect, source, Color.White);
            }
        }

        #endregion

        internal void Clear()
        {
            if (visible)
            {
                visible = false;
                visibleDotCounter--;
                SoundManager.PlayDotSound();
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
        }
    }
}
