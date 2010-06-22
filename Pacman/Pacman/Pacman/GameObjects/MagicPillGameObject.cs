using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Pacman.GameManager;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman.GameObjects
{
    class MagicPillGameObject : GameObject
    {
        const int DOT_SIZE = 8;
        bool visible;

        static int width = 24;
        static int height = 17;

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


        static public List<GameObject> Generate()
        {
            
            int x = random.Next(width);
            int y = random.Next(height);

            int[,] tab = new int [8,2];

            tab[0,0] = x; tab[0,1] = y;
            tab[1,0] = width-x; tab[1,1] = y;

            tab[2,0] = x; tab[2,1] = height-y;
            tab[3,0] = width-x; tab[3,1] = height-y;

            x = random.Next(width);
            y = random.Next(height);

            tab[4,0] = x; tab[4,1] = y;
            tab[5,0] = width-x; tab[5,1] = y;

            tab[6,0] = x; tab[6,1] = height-y;
            tab[7,0] = width-x; tab[7,1] = height-y;

            
            List<GameObject> list = new List<GameObject>();
            for (int i = 0; i < 8; i++)
            {
                list.Add(new MagicPillGameObject(tab[i,0], tab[i,1]));
            }

            return list;

        }


        private MagicPillGameObject(int x, int y)
        {
            screenVectorPosition = (new GameCoordinates(x, y)).GetScreenPosition();
            screenVectorPosition += new Vector2(4, 4);
            visible = true;
        }


        public override void LoadContent()
        {
            this.source = new Rectangle(18 * OBJECT_SIZE+4, 4 * OBJECT_SIZE+4, 2 * DOT_SIZE, 2 * DOT_SIZE);
        }

        #region Draw

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (visible)
            {
                Rectangle rect = new Rectangle((int)screenVectorPosition.X,
                                               (int)screenVectorPosition.Y,
                                               2 * DOT_SIZE,
                                               2 * DOT_SIZE);

                GameObject.SpriteBatch.Draw(sprite, rect, source, Color.White);
            }
        }

        internal void Clear()
        {
            if (visible)
            {
                visible = false;
                SoundManager.PlayPillSound();
            }
        }

        #endregion

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

        }
    }
}
