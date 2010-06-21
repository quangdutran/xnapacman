using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pacman.GameObjects
{
    class ToolBarGameObject : GameObject
    {

        int delta, lastUpdate;
        int points;
        bool levelCompleted;

        public ToolBarGameObject()
        {
            points = 0;
        }

        public override void LoadContent()
        {

            lastUpdate = DotGameObject.VisibleDotCounter-1;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

            delta = lastUpdate - DotGameObject.VisibleDotCounter;

            points += delta*10;

            lastUpdate = DotGameObject.VisibleDotCounter;

            if (delta>0)
            { 
                //System.Console.WriteLine("VisibleDotCounter: " + DotGameObject.VisibleDotCounter.ToString());
            }

            if (DotGameObject.VisibleDotCounter == 0)
            { 
                //levelCompleted = true;
                
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            GameObject.SpriteBatch.DrawString(gameFontCourierNew20, "Points: " + points.ToString(), new Vector2(24, 19 * 24), Color.White);

            if (levelCompleted)
            {
                String msg = "Level completed!";
                Vector2 vec = gameFontCourierNew40.MeasureString(msg);
                Color color;
                if (gameTime.TotalGameTime.Milliseconds  < 500)
                    color = Color.White;
                else
                    color = Color.LightGreen;

                GameObject.SpriteBatch.DrawString(gameFontCourierNew40, msg, new Vector2(640 / 2 - vec.X / 2, 480 / 2 - vec.Y / 2), color);
            }
        }
    }
}
