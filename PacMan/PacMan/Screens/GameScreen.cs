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
        Texture2D life;

        SpriteFont hudfont;

        Vector2 pacPos = new Vector2(PacManGame.TILE_SIZE, PacManGame.TILE_SIZE);

        PacMans pacman;
        List<string> strings = new List<string>();
        List<Walls> walls = new List<Walls>();
        List<Ghosts> ghosts = new List<Ghosts>();
        List<Coins> coins = new List<Coins>();

        Coins.AddPoints addPoints;

        public static int ghostColor = 1;
        int numberOfLives = 2;
        public static int points = 0;
        private Vector2 pacmanRespawnPos;
        private Vector2 ghostRespawnPos;
        bool allCoinsEaten = false;

        public GameScreen(PacManGame game)
        {
            this.game = game;
            addPoints = new Coins.AddPoints(AddPoints);
        }

        public void Load()
        {
            LoadPictures();
            CreateObjectFactory();
            LoadFonts();
        }

        private void LoadPictures()
        {
            pacSprite = game.Content.Load<Texture2D>(@"pacman");
            wall = game.Content.Load<Texture2D>(@"wall");
            ghost = game.Content.Load<Texture2D>(@"ghost");
            coin = game.Content.Load<Texture2D>(@"coin");
            life = game.Content.Load<Texture2D>(@"lifes");
        }

        private void LoadFonts()
        {
            hudfont = game.Content.Load<SpriteFont>(@"hudfont");
        }

        private void CreateObjectFactory()
        {
            StreamReader sr = new StreamReader(@"testlevel.txt");
            int row = 0;
            while (!sr.EndOfStream)
            {
                string objectStr = sr.ReadLine();
                for (int col = 0; col < objectStr.Length; col++)
                {
                    ObjectFactory(objectStr[col], row, col);
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
                    pacmanRespawnPos = pos;
                    pacman = new PacMans(pacSprite, pos );
                    break;
                case 'G':
                    ghostRespawnPos = pos;
                    ghosts.Add(new Ghosts(ghost, pos));
                    break;
                case 'C':
                    coins.Add(new Coins(coin, new Vector2(pos.X + 15, pos.Y + 15), addPoints));
                    break;
            }

        }

        public void Update(GameTime gameTime)
        {
            pacman.Update(walls);
            foreach (Ghosts ghost in ghosts)
            {
                ghost.Update(walls, ghostColor);
                ghostColor++;
                if (ghostColor > 3)
                {
                    ghostColor = 1;
                }
                if (ghost.Hit(pacman.PosRect))
                {
                    pacman.Respawn(pacmanRespawnPos);
                    ghost.Respawn(ghostRespawnPos);
                    numberOfLives--;
                } 
            }
            bool aCoinEaten = true;
            foreach (Coins coin in coins)
            {
                coin.Hit(pacman.PosRect);
                if (!coin.Eaten())
                {
                    aCoinEaten = false;
                }
            }
            allCoinsEaten = aCoinEaten;
            lifeSource();
            EndGame();
            Console.WriteLine(numberOfLives);
        }

        public void AddPoints(int points)
        {
            GameScreen.points += points;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawWalls(spriteBatch);
            DrawCoins(spriteBatch);
            DrawGhosts(spriteBatch);
            pacman.Draw(spriteBatch);
            DrawFonts(spriteBatch);
            DrawLives(spriteBatch);
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

        private void DrawCoins(SpriteBatch spritebatch)
        {
            foreach (Coins coin in coins)
            {
                coin.Draw(spritebatch);
            }
        }

        private void DrawLives(SpriteBatch spritebatch)
        {
            Rectangle lifeRect = new Rectangle(PacManGame.TILE_SIZE * 2, PacManGame.TILE_SIZE * 15, PacManGame.TILE_SIZE * numberOfLives, PacManGame.TILE_SIZE);
            spritebatch.Draw(life, lifeRect, lifeSource(), Color.White);
        }

        private void DrawFonts(SpriteBatch spritebatch)
        {
            string lives = "Lives: ";
            string score = "Score: " + points;
            Vector2 livesLen = hudfont.MeasureString(lives);
            Vector2 scoreLen = hudfont.MeasureString(score);
            spritebatch.DrawString(hudfont, lives, new Vector2(PacManGame.TILE_SIZE, PacManGame.TILE_SIZE * 15 + livesLen.Y / 2), Color.White);
            spritebatch.DrawString(hudfont, score, new Vector2(PacManGame.TILE_SIZE * 10, PacManGame.TILE_SIZE * 15 + scoreLen.Y / 2), Color.White);
        }
        private Rectangle lifeSource()
        {
            Rectangle lifeSrcRect = new Rectangle(0, 0, life.Width /2 * numberOfLives, life.Height);
            return lifeSrcRect;
        }
        private void EndGame()
        {
            if (numberOfLives < 0 || allCoinsEaten)
            {
                game.SetScreen(PacManGame.GameState.EndScreen);
            }
        }
    }
}
