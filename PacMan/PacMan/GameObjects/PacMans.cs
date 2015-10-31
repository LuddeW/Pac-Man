using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacMan.GameObjects.StillObjects;
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
        Vector2 originPos = new Vector2(0, 0);

        public PacMans(Texture2D texture, Vector2 pos) : base(texture, pos)
        {
            this.pos = pos;
            this.texture = texture;
            texEffects = SpriteEffects.None;
            clock = new Clock();
        }

        // Override?
        public void Update(List<Walls> walls)
        {
            PacMovement(walls);
            clock.AddTime(0.03F);

            srcRect = new Rectangle((texture.Width / 4) * PacAnimation(), 0, texture.Width / 4, texture.Height);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            UpdateOriginPos();
            spriteBatch.Draw(texture, pos, srcRect, Color.White, rotation, originPos, scale, texEffects, 1f);
        }

        private void PacMovement(List<Walls> walls)
        {
            Vector2 newPacPos = new Vector2(pos.X, pos.Y);
            float newRot = rotation;
            SpriteEffects newEffect = texEffects;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                newEffect = SpriteEffects.None;
                newPacPos.X += 1;
                newRot = MathHelper.ToRadians(0);
                movement = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                newEffect = SpriteEffects.FlipHorizontally;
                newPacPos.X -= 1f;
                newRot = MathHelper.ToRadians(0);
                movement = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                newEffect = SpriteEffects.None;
                newPacPos.Y -= 1f;
                newRot = MathHelper.ToRadians(-90);
                movement = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                newEffect = SpriteEffects.FlipHorizontally;
                newPacPos.Y += 1f;
                newRot = MathHelper.ToRadians(-90);
                movement = true;
            }
            else
            {
                movement = false;
                pacAnimation = 1;
            }
            if (!Collision(newPacPos, walls))
            {
                pos = newPacPos;
                rotation = newRot;
                texEffects = newEffect;
            }
            else
            {

            }

        }

        private void UpdateOriginPos()
        {
            if (((int)rotation) == 0)
            {
                originPos.X = 0;
                originPos.Y = 0;
            }
            else
            {
                originPos.X = 40;
                originPos.Y = 0;
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
        private bool Collision(Vector2 newPacPos, List<Walls> walls)
        {
            Rectangle temp = new Rectangle((int)newPacPos.X, (int)newPacPos.Y, 40, 40);
            foreach (Walls wall in walls)
            {
                if (wall.Rect.Intersects(temp))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
