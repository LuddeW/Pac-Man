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

        Vector2 pacPos = new Vector2(40, 40);
        Rectangle pacHitPos;
        Rectangle wallRect;
        PacMans pacman;
        List<string> strings = new List<string>();
        List<Walls> walls = new List<Walls>();     

        int pacAnimation = 0;
        bool movement = false;

        public GameScreen(PacManGame game)
        {
            this.game = game;
        }

        public void Load()
        {
            pacSprite = game.Content.Load<Texture2D>(@"pacman");
            wall = game.Content.Load<Texture2D>(@"Wall-test");
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
            }

        }

        public void Update(GameTime gameTime)
        {
            pacman.Update(walls);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           // wallRect = new Rectangle(100, 100, wall.Width * 4, wall.Height * 4);
            pacman.Draw(spriteBatch);
            DrawWalls(spriteBatch);
        }

        private void DrawWalls(SpriteBatch spriteBatch)
        {
            foreach (Walls wall in walls)
            {
                wall.Draw(spriteBatch);
            }
        }
    }
}
