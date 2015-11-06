using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PacMan.GameObjects.StillObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan.GameObjects
{
    class Ghosts:MovingObjects
    {
        Clock clock = new Clock();

        Rectangle srcRect;
 //       Rectangle posRect;

       

        int ghostAnimation = 0;
        int ghostSprite = 1;
        static int ghostSpriteIx = 1;

        public Ghosts(Texture2D texture, Vector2 pos) : base(texture, pos)
        {
            this.texture = texture;
            ghostSprite = ghostSpriteIx;
            ghostSpriteIx++;
            if(ghostSpriteIx > 4)
            {
                ghostSpriteIx = 1;
            }
        }

        public void Update(List<Walls> walls)
        {
            MoveObject(walls);
            srcRect = new Rectangle(texture.Width / 8 * GhostAnimation(), texture.Height / 7 * ghostSprite, texture.Width / 8, texture.Height / 7);
            clock.AddTime(0.01F);
        }

        public override bool TestDirectionChange(List<Walls> walls)
        {
            Vector2 newGhostPos = new Vector2(Pos.X, Pos.Y);
            Direction newDirecton = CurrentState;
            if (Rndom() <= 24)
            {
                newDirecton = Direction.RIGHT;               
                newGhostPos.X += speed;
            }
            else if (Rndom() > 24 && Rndom() <= 49)
            {
                newDirecton = Direction.LEFT;               
                newGhostPos.X -= speed;
            }
            else if (Rndom() > 49 && Rndom() <= 74)
            {
                newDirecton = Direction.UP;                
                newGhostPos.Y -= speed;
            }
            else if (Rndom() > 74)
            {
                newDirecton = Direction.DOWN;
                newGhostPos.Y += speed;
            }
            else
            {
                return false;
            }
            if (!Collision(newGhostPos, walls))
            {
                Pos = newGhostPos;
                CurrentState = newDirecton;
                return true;
            }
            else
            {
                NewRndom();
                return false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, PosRect, srcRect, Color.White);
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
