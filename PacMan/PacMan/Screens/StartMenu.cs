using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan.Screens
{
    class StartMenu
    {
        PacManGame game;

        Clock clock = new Clock();

        SpriteFont menufont;

        protected enum ButtonState { Start, LevelEditor, Highscore}
        ButtonState CurrentState = ButtonState.Start;

        Texture2D ghost;

        Rectangle posRect;
        Rectangle srcRect;

        string start = "START";
        string leveleditor = "LEVEL-EDITOR";
        string highscore = "HIGHSCORE";
        int ghostAnimation = 0;

        public StartMenu(PacManGame game, SpriteFont MenuFont)
        {
            this.game = game;
            this.menufont = MenuFont;

        }

        public void LoadPictures()
        {
            ghost = game.Content.Load<Texture2D>(@"ghost");
        }

        public void Update(KeyboardState keyState, KeyboardState prevKeyState)
        {
            switch (CurrentState)
            {
                case ButtonState.Start:
                    posRect = new Rectangle(game.Window.ClientBounds.Width / 4 - 2 * PacManGame.TILE_SIZE, PacManGame.TILE_SIZE * 4 + 5, PacManGame.TILE_SIZE, PacManGame.TILE_SIZE);
                    break;
                case ButtonState.LevelEditor:
                    posRect = new Rectangle(game.Window.ClientBounds.Width / 4 - 2 * PacManGame.TILE_SIZE, PacManGame.TILE_SIZE * 7 + 5, PacManGame.TILE_SIZE, PacManGame.TILE_SIZE);
                    break;
                case ButtonState.Highscore:
                    posRect = new Rectangle(game.Window.ClientBounds.Width / 4 - 2 * PacManGame.TILE_SIZE, PacManGame.TILE_SIZE * 10 + 5, PacManGame.TILE_SIZE, PacManGame.TILE_SIZE);
                    break;
            }
            clock.AddTime(0.1F);
            srcRect = new Rectangle(ghost.Width / 8 * GhostAnimation(), ghost.Height / 7 * 1, ghost.Width / 8, ghost.Height / 7);
            HandleMenu(keyState, prevKeyState);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            DrawPacman(spritebatch);
            DrawFonts(spritebatch);
        }

        protected void DrawFonts(SpriteBatch spritebatch)
        {
            spritebatch.DrawString(menufont, start, new Vector2(game.Window.ClientBounds.Width / 4, PacManGame.TILE_SIZE * 4), Color.White);
            spritebatch.DrawString(menufont, leveleditor, new Vector2(game.Window.ClientBounds.Width / 4, PacManGame.TILE_SIZE * 7), Color.White);
            spritebatch.DrawString(menufont, highscore, new Vector2(game.Window.ClientBounds.Width / 4, PacManGame.TILE_SIZE * 10), Color.White);

        }

        protected void DrawPacman(SpriteBatch spritebatch)
        {
            spritebatch.Draw(ghost, posRect, srcRect, Color.White);
        }

        protected void HandleMenu(KeyboardState keyState, KeyboardState prevKeyState)
        {
            if (CurrentState == ButtonState.Start )
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter)) 
                {
                    game.SetScreen(PacManGame.GameState.GameScreen);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down) && prevKeyState.IsKeyUp(Keys.Down))
                {
                    CurrentState = ButtonState.LevelEditor;
                }
                prevKeyState = keyState;
            }
            if (CurrentState == ButtonState.LevelEditor)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    //game.SetScreen(PacManGame.GameState.LevelEditor);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Up) && prevKeyState.IsKeyUp(Keys.Up))
                {
                    CurrentState = ButtonState.Start;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down) && prevKeyState.IsKeyUp(Keys.Down))
                {
                    CurrentState = ButtonState.Highscore;
                }
                prevKeyState = keyState;
            }
            
            if (CurrentState == ButtonState.Highscore)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    game.SetScreen(PacManGame.GameState.HighScore);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && prevKeyState.IsKeyUp(Keys.Up))
                {
                    CurrentState = ButtonState.LevelEditor;
                }
                prevKeyState = keyState;
            }
        }

        private int GhostAnimation()
        {
            if (clock.Timer() > 1f)
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
