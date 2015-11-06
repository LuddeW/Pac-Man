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

        private int coins;
        private Rectangle srcRect;
        private Rectangle dstRect;

        public Coins(Texture2D texture, Texture2D test, Vector2 pos, int coins) : base(texture, pos)
        {
            this.coins = coins;
            srcRect = new Rectangle(0, 0, texture.Width, texture.Height);
            dstRect = new Rectangle(PosRect.Location, PosRect.Size);
            if (coins <= 10)
            {
                dstRect.Inflate(-15, -15);
            }
            else
            {
                dstRect.Inflate(-10, -10);
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                spriteBatch.Draw(texture, dstRect, srcRect, Color.White);
            }

        }

        public bool Hit(Rectangle hitRect)
        {
            Rectangle tempHitRect = new Rectangle(hitRect.Location, hitRect.Size);
            Rectangle temp = new Rectangle(PosRect.Location, PosRect.Size);
            tempHitRect.Inflate(-19, -19);
            temp.Inflate(-19, -19);
            if (temp.Intersects(tempHitRect))
            {
                visible = false;
                return true;
            }   
            else
            {
                return false;
            }  
        }
    }
}
