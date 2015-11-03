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

        Texture2D test;

        public Coins(Texture2D texture, Texture2D test, Vector2 pos) : base(texture, pos)
        {
            this.test = test;
        }   
          
        public override void Draw(SpriteBatch spritebatch)
        {
            if (visible)
            {
                spritebatch.Draw(texture, PosRect, Color.White);
            }
            
        }

        public bool Hit(Rectangle hitRect)
        {
            if (PosRect.Intersects(hitRect))
            {
                visible = false;
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
    }
}
