using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan.GameObjects.StillObjects
{
    class Coins : GameObject
    {
        public bool visible = true;

        AddPoints addPoints;

        public Coins(Texture2D texture, Vector2 pos, AddPoints addPoints) : base(texture, pos)
        {
            
            this.addPoints = addPoints;
        }

        public delegate void AddPoints(int points);
          
        public override void Draw(SpriteBatch spritebatch)
        {
            if (visible)
            {
                spritebatch.Draw(texture, PosRect, Color.White);
            }
            
        }

        public bool Hit(Rectangle hitRect)
        {
            if (PosRect.Intersects(hitRect) && visible)
            {
                visible = false;
                addPoints(10);
                return true;
            }   
            else
            {
                return false;
            }  
        }

        public override void SetPosRect()
        {
            PosRect = new Rectangle((int)Pos.X, (int)Pos.Y, texture.Width, texture.Height);
        }

        public bool Eaten()
        {
            return !visible;
        }
    }
}
