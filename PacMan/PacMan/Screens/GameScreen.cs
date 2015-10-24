using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan.Screens
{
    class GameScreen
    {
        PacManGame game;

        Clock clock = new Clock();
        Texture2D pacSprite;

        Vector2 pos = new Vector2(20, 20);

        int pacAnimation = 0;
        float scale = 1f;
        float rotation = 0;
        bool movement = false;

        SpriteEffects pacEffects = SpriteEffects.None;

        public GameScreen(PacManGame game)
        {
            this.game = game;
        }

        public void LoadPictures()
        {
            pacSprite = game.Content.Load<Texture2D>(@"pacman");
        }

        public void Update(GameTime gameTime)
        {
            PacMovement();
            clock.AddTime((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        private void PacMovement()
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                pacEffects = SpriteEffects.None;
                pos.X += 1f;
                rotation = MathHelper.ToRadians(0);
                movement = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                pacEffects = SpriteEffects.FlipHorizontally;
                pos.X -= 1f;
                rotation = MathHelper.ToRadians(0);
                movement = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                pacEffects = SpriteEffects.None;
                pos.Y -= 1f;
                rotation = MathHelper.ToRadians(-90);
                movement = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                pacEffects = SpriteEffects.FlipHorizontally;
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pacSprite, pos, new Rectangle((pacSprite.Width / 4) * PacAnimation(), 0, pacSprite.Width / 4, pacSprite.Height), Color.White, rotation, new Vector2(20, 20), scale, pacEffects, 1f);
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
