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
        enum Direction
        {
            LEFT, RIGHT, UP, DOWN
        }
        //Texture2D texture;
        //Vector2 pos;
        Rectangle srcRect;
        float rotation = 0f;
        float horizSpeed = 0f;
        float vertSpeed = 0f;
        float scale = 1f;
        int pacAnimation = 1;
        SpriteEffects texEffects;
        bool movement = false;
        Clock clock;
        private Vector2 originPos;
        Direction currentDirection = Direction.LEFT;
        private readonly float pacmanSpeed = 1f;
        SpriteSheet spriteSheet;

        public PacMans(Texture2D texture, Vector2 pos) : base(texture, pos)
        {
            texEffects = SpriteEffects.None;
            spriteSheet = new SpriteSheet(4, 1, texture);
            clock = new Clock();
            originPos = new Vector2(0, 0);
            ChangeDirection(currentDirection);
        }
 
        public Rectangle Rect
        {
            get
            {
                return Rect2(pos);
            }
        }

        public Rectangle Rect2(Vector2 pos)
        {
            return new Rectangle((int)pos.X, (int)pos.Y, PacManGame.TILE_SIZE, PacManGame.TILE_SIZE);
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
//            spriteBatch.Draw(texture, pos, srcRect, Color.White, rotation, originPos, scale, texEffects, 1f);

            spriteBatch.Draw(texture, pos, spriteSheet.SrcRect( PacAnimation()), Color.White, rotation, originPos, scale, texEffects, 1f);
        }

        private void PacMovement(List<Walls> walls)
        {
            Vector2 newPacPos = new Vector2(pos.X, pos.Y);
            newPacPos.X += horizSpeed;
            newPacPos.Y += vertSpeed;
            if (!Collision(newPacPos, walls))
            {
                pos = newPacPos;
            }
            else
            {
                movement = false;
                pacAnimation = 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                ChangeDirection(Direction.RIGHT);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                ChangeDirection(Direction.LEFT);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                ChangeDirection(Direction.UP);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                ChangeDirection(Direction.DOWN);
            }
        }

        private void ChangeDirection(Direction changeDirection)
        {
            movement = true;
            switch (changeDirection)
            {
                case Direction.RIGHT:
                    texEffects = SpriteEffects.None;
                    horizSpeed = pacmanSpeed;
                    vertSpeed = 0f;
                    rotation = MathHelper.ToRadians(0);
                    break;
                case Direction.LEFT:
                    texEffects = SpriteEffects.FlipHorizontally;
                    horizSpeed = -pacmanSpeed;
                    vertSpeed = 0f;
                    rotation = MathHelper.ToRadians(0);
                    break;
                case Direction.UP:
                    texEffects = SpriteEffects.None;
                    vertSpeed = -pacmanSpeed;
                    horizSpeed = 0f;
                    rotation = MathHelper.ToRadians(-90);
                    break;
                case Direction.DOWN:
                    texEffects = SpriteEffects.FlipHorizontally;
                    vertSpeed = pacmanSpeed;
                    horizSpeed = 0f;
                    rotation = MathHelper.ToRadians(-90);
                    break;
            }
        }

        private void UpdateOriginPos()
        {
            if(((int)rotation)==0)
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
            foreach (Walls wall in walls)
            {
                if (wall.Rect.Intersects(Rect2(newPacPos)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
