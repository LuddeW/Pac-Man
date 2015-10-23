using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        int pacAnimation = 0;

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
            clock.AddTime((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pacSprite, new Vector2(0, 0), new Rectangle((pacSprite.Width / 4) * PacAnimation(), 0, pacSprite.Width / 4, pacSprite.Height), Color.White);
        }

        private int PacAnimation()
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

            return pacAnimation;
        }
    }
}
