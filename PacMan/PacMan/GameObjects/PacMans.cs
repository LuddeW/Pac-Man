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
    class PacMans:MovingObjects
    {
        Clock clock;

        Rectangle srcRect;
        Vector2 originPos = new Vector2(0, 0);

        float rotation = 0f;
        float scale = 1f;
        int pacAnimation = 1;
        bool movement = true;
        SpriteEffects texEffects;
        private bool pacmanDead = false;

        public PacMans(Texture2D texture, Vector2 pos) : base(texture, pos)
        {
            this.Pos = pos;
            this.texture = texture;
            texEffects = SpriteEffects.None;
            clock = new Clock();
        }

        public void Update(List<Walls> walls, bool pacmanDead)
        {
            this.pacmanDead = pacmanDead;
            if(!pacmanDead)
            {
	            MoveObject(walls);
	            PacTeleport();
            }
            else
            {
                movement = false;
            }
            clock.AddTime(0.03F);
            srcRect = new Rectangle((texture.Width / 6) * PacAnimation(), 0, texture.Width / 6, texture.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            UpdateOriginPos();
            spriteBatch.Draw(texture, Pos, srcRect, Color.White, rotation, originPos, scale, texEffects, 1f);
        }

        public override bool TestDirectionChange(List<Walls> walls)
        {
            Vector2 newPacPos = new Vector2(Pos.X, Pos.Y);
            float newRot = rotation;
            SpriteEffects newEffect = texEffects;
            Direction newDirecton = CurrentState;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                newDirecton = Direction.RIGHT;
                newEffect = SpriteEffects.None;
                newPacPos.X += speed;
                newRot = MathHelper.ToRadians(0);
                movement = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                newDirecton = Direction.LEFT;
                newEffect = SpriteEffects.FlipHorizontally;
                newPacPos.X -= speed;

                newRot = MathHelper.ToRadians(0);
                movement = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                newDirecton = Direction.UP;
                newEffect = SpriteEffects.None;
                newPacPos.Y -= speed;
                newRot = MathHelper.ToRadians(-90);
                movement = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                newDirecton = Direction.DOWN;
                newEffect = SpriteEffects.FlipHorizontally;
                newPacPos.Y += speed;
                newRot = MathHelper.ToRadians(-90);
                movement = true;
            }
            else
            {
                return false;
            }
            if (!Collision(newPacPos, walls))
            {
                Pos = newPacPos;
                rotation = newRot;
                texEffects = newEffect;
                CurrentState = newDirecton;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void PacTeleport()
        {
            if (Pos.X < - PacManGame.TILE_SIZE - 1)
            {
                SetPosX(PacManGame.TILE_SIZE * 15 - 2);
            }
            else if (Pos.X > 600)
            {
                SetPosX(-PacManGame.TILE_SIZE + 2); 
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
            else if (pacmanDead)
            {
                if(pacAnimation < 4)
                {
                    pacAnimation = 4;
                    clock.ResetTime();
                }
                if (clock.Timer() > 1f)
                {
                    pacAnimation++;
                    clock.ResetTime();
                    if (pacAnimation > 5)
                    {
                        pacAnimation = 5;
                    }
                }
            }
            return pacAnimation;
        }

        public override void Respawn(Vector2 respawnPos)
        {            
            Pos = respawnPos;
            pacAnimation = 1;
            movement = true;
        }

    }
}
