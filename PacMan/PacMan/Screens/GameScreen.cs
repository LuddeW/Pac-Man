using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PacMan.GameObjects;
using PacMan.GameObjects.StillObjects;
using System.IO;

namespace PacMan.Screens
{
    class GameScreen
    {
        PacManGame game;

        Clock clock = new Clock();

        Texture2D pacSprite;
        Texture2D wall;
        Texture2D ghost;
        Texture2D coin;

        Vector2 pacPos = new Vector2(40, 40);
   
        PacMans pacman;
        List<string> strings = new List<string>();
        List<Walls> walls = new List<Walls>();
        List<Ghosts> ghosts = new List<Ghosts>();    

        public GameScreen(PacManGame game)
        {
            this.game = game;
        }

        public void Load()
        {
            pacSprite = game.Content.Load<Texture2D>(@"pacman");
            wall = game.Content.Load<Texture2D>(@"Wall-test");
            ghost = game.Content.Load<Texture2D>(@"ghost");
            coin = game.Content.Load<Texture2D>(@"coin");
            StreamReader sr = new StreamReader(@"testlevel.txt");
            int row = 0;
            while (!sr.EndOfStream)
            {
                string objectStr = sr.ReadLine();
                for (int col = 0; col < objectStr.Length; col++)
                {
                    ObjectFactory(objectStr[col] , row, col);
                }
                row++;
            }

        }

        private void ObjectFactory(char objectChar, int row, int col)
        {
            Vector2 pos = new Vector2(PacManGame.TILE_SIZE * col, PacManGame.TILE_SIZE * row);
            switch (objectChar)
            {
                case 'W':
                    walls.Add(new Walls(wall, pos));
                    break;
                case 'P':
                    pacman = new PacMans(pacSprite, pos);
                    break;
                case 'G':
                    ghosts.Add(new Ghosts(ghost, pos));
                    break;
            }

        }

        public void Update(GameTime gameTime)
        {
            pacman.Update(walls);
            foreach (Ghosts ghost in ghosts)
            {
                ghost.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            pacman.Draw(spriteBatch);
            DrawWalls(spriteBatch);
            DrawGhosts(spriteBatch);
        }

        private void DrawWalls(SpriteBatch spriteBatch)
        {
            foreach (Walls wall in walls)
            {
                wall.Draw(spriteBatch);
            }
        }
        private void DrawGhosts(SpriteBatch spritebatch)
        {
            foreach (Ghosts ghost in ghosts)
            {
                ghost.Draw(spritebatch);
            }
        }
    }
}
