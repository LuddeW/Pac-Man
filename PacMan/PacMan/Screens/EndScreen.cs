using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan.Screens
{
    class EndScreen
    {
        PacManGame game;

        Clock clock = new Clock();

        SpriteFont menuFont;
        string score;

        public EndScreen(PacManGame game, SpriteFont menuFont)
        {
            this.game = game;
            this.menuFont = menuFont;
        }

        public void Update()
        {
            clock.AddTime(0.01f);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            score = "Score:" + GameScreen.points;
            Vector2 scoreLen = menuFont.MeasureString(score);
            spriteBatch.DrawString(menuFont, score, new Vector2(game.Window.ClientBounds.Width / 2 - scoreLen.X / 2, game.Window.ClientBounds.Height / 2 - scoreLen.Y / 2), Color.White);
        }

        public bool ChangeScreen()
        {
            if (clock.Timer() > 5f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Init()
        {
            clock.ResetTime();
        }
    }
}
