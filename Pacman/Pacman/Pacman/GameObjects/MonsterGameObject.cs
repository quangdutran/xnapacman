using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameStateManagement;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        

        public MonsterGameObject(MonsterGameObjectColor color)
        {
            monsterColor = color;
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
            //TODO: move to another place
            screenPosition = new Vector2(0.0f + random.Next(5) * 100, 0.0f + random.Next(5) * 100);

       }


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            throw new NotImplementedException();
        }

        public override void HandleInput(KeyboardState keyboardState)
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime)
        {
            
            Color color = new Color(255, 255, 255, 255);

            Rectangle monsterRectangle = new Rectangle((int)screenPosition.X, (int)screenPosition.Y, SCALE*OBJECT_SIZE, SCALE*OBJECT_SIZE);

            GameObject.SpriteBatch.Draw(sprite, monsterRectangle, source, color);

            
        }
    }
}
