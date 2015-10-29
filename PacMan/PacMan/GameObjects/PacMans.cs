using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan.GameObjects
{
    class PacMans:GameObject
    {
        //Texture2D texture;
        //Vector2 pos;
        Rectangle srcRect;
        float rotation = 0f;
        float scale = 1f;
        int pacAnimation = 1;
        SpriteEffects texEffects;
        bool movement = false;
        Clock clock;

        public PacMans(Texture2D texture, Vector2 pos) : base(texture, pos)
        {
            this.pos = pos;
            this.texture = texture;
            texEffects = SpriteEffects.None;
            clock = new Clock();
        }

        // Override?
        public void Update()
        {
            PacMovement();
            clock.AddTime(0.03F);

            srcRect = new Rectangle((texture.Width / 4) * PacAnimation(), 0, texture.Width / 4, texture.Height);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, srcRect, Color.White, rotation, new Vector2(20, 20), scale, texEffects, 1f);
        }

        private void PacMovement()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                texEffects = SpriteEffects.None;
                pos.X += 1;
                rotation = MathHelper.ToRadians(0);
                movement = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                texEffects = SpriteEffects.FlipHorizontally;
                pos.X -= 1f;
                rotation = MathHelper.ToRadians(0);
                movement = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                texEffects = SpriteEffects.None;
                pos.Y -= 1f;
                rotation = MathHelper.ToRadians(-90);
                movement = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                texEffects = SpriteEffects.FlipHorizontally;
                pos.Y += 1f;
                rotation = MathHelper.ToRadians(-90);
                movement = true;
            }
            else
            {
                movement = false;
                pacAnimation = 1;
            }
            
        }
        private int PacAnimation()
        {
            if (movement)
            {
                if (clock.Timer() > 0.2f)
                {
                    pacAnimation++;
                    clock.ResetTime();
                    if (pacAnimation > 3)
                    {
                        pacAnimation = 0;
                    }
                }
            }
            
            return pacAnimation;
        }
    }
}
