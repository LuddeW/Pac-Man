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
        Rectangle posRect;
        public Coins(Texture2D texture, Vector2 pos) : base(texture, pos)
        {
            posRect = new Rectangle((int)pos.X, (int)pos.Y, texture.Width, texture.Height);
        }     
        public void DrawCoin(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, posRect, Color.White);
        }
    }
}
