using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameStateManagement;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman.GameObjects
{
    class MonsterGameObject: GameObject
    {

        public enum MonsterGameObjectColor
        { 
          Blue,
          Green,
          Pink,
          Red
        }

        MonsterGameObjectColor monsterColor;
        Rectangle source;

        public MonsterGameObject(MonsterGameObjectColor color)
        {
            monsterColor = color;
        }


        public override void LoadContent()
        {
            base.LoadContent(); //loading sprite

            switch (monsterColor)
            { 
                case MonsterGameObjectColor.Blue:
                    this.source = new Rectangle(0, 0, OBJECT_SIZE, OBJECT_SIZE);
                break;

                case MonsterGameObjectColor.Green:
                    this.source = new Rectangle(5, 0, OBJECT_SIZE, OBJECT_SIZE);
                break;

                case MonsterGameObjectColor.Pink:
                    this.source = new Rectangle(10, 0, OBJECT_SIZE, OBJECT_SIZE);    
                break;

                case MonsterGameObjectColor.Red:
                    this.source = new Rectangle(15, 0, OBJECT_SIZE, OBJECT_SIZE);
                break;

            }
            //TODO: move to another place
            screenPosition = new Vector2(0.0f + random.Next(5) * 10, 0.0f + random.Next(5) * 10);

       }


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            throw new NotImplementedException();
        }

        public override void HandleInput(InputState input)
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            Color color = new Color(255, 255, 255, 255);


            Rectangle monsterRectangle = new Rectangle((int)screenPosition.X, (int)screenPosition.Y, SCALE*OBJECT_SIZE, SCALE*OBJECT_SIZE);

            spriteBatch.Draw(sprite, monsterRectangle, source, color);

            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0)
                ScreenManager.FadeBackBufferToBlack(255 - TransitionAlpha);
        }
    }
}
