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
        public Ghosts(Texture2D texture, Vector2 pos) : base(texture, pos)
        {
            this.texture = texture;
            this.pos = pos;
        }

        public void Update()
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, Color.White);
        }
    }
}
