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
        PacManGame Game;

        Texture2D menuDoge;
        SpriteFont MenuFont;

        protected enum ButtonState { Start, LevelEditor, Highscore}
        ButtonState CurrentState = ButtonState.Start;

        string start = "Start";
        string levelEditor = "Level-Editor";
        string highscore = "Highscore";



        public StartMenu(PacManGame Game, SpriteFont MenuFont)
        {
            this.Game = Game;
            this.MenuFont = MenuFont;

            LoadPictures();
        }

        protected void LoadPictures()
        {
            menuDoge = Game.Content.Load<Texture2D>(@"menudoge75x75");
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch sb)
        {
            
            switch (CurrentState)
            {
                case ButtonState.Start:
                    
                    break;
                case ButtonState.LevelEditor:
                    
                    break;
                case ButtonState.Highscore:
                    
                    break;
            }
            DrawFonts(sb);
        }

        protected void DrawFonts(SpriteBatch sb)
        {
           
        }
        protected void HandleMenu()
        {
            if (CurrentState == ButtonState.Start )
            {

            }
        }
    }
}
