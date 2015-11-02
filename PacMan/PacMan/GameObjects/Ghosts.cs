using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan.GameObjects
{
    class Ghosts:GameObject
    {
        Rectangle srcRect;
        public Ghosts(Texture2D texture, Vector2 pos) : base(texture, pos)
        {
            this.texture = texture;
            this.pos = pos;
        }

        public void Update()
        {
            srcRect = new Rectangle(texture.Width / 8 * 0, texture.Height / 7 * 1, texture.Width / 8, texture.Height / 7);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, srcRect, Color.White);
        }
    }
}
