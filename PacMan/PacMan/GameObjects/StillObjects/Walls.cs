using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan.GameObjects.StillObjects
{
    class Walls:GameObject
    {
        public Walls(Texture2D texture, Vector2 pos) : base(texture, pos)
        {
            this.texture = texture;
            this.pos = pos;
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y, texture.Width, texture.Height), Color.White);
        }
    }
}
