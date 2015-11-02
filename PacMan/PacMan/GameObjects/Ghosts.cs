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
        Clock clock = new Clock();

        Rectangle srcRect;
        Rectangle posRect;

        int ghostAnimation = 0;

        public Ghosts(Texture2D texture, Vector2 pos) : base(texture, pos)
        {
            this.texture = texture;
            this.pos = pos;
            posRect = new Rectangle((int)pos.X, (int)pos.Y, PacManGame.TILE_SIZE, PacManGame.TILE_SIZE);
        }

        public void Update()
        {
            
            srcRect = new Rectangle(texture.Width / 8 * GhostAnimation(), texture.Height / 7 * 1, texture.Width / 8, texture.Height / 7);
            clock.AddTime(0.01F);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, posRect, srcRect, Color.White);
        }

        private int GhostAnimation()
        {
            if (clock.Timer() > 0.2f)
            {
                ghostAnimation++;
                clock.ResetTime();
                if (ghostAnimation > 7)
                {
                    ghostAnimation = 0;
                }
            }
            return ghostAnimation;
            
        }
    }
}
