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

        public static int ghostColor = 1;
        int numberOfLives = 3;
        string lives = "Lives:";
        private bool pacmanDead = false;
        private double pacmanDeathTime = 0;
        private Vector2 pacmanRespawnPos;
        private Vector2 ghostRespawnPos;

        public GameScreen(PacManGame game)
        {
            this.game = game;
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
                    coins.Add(new Coins(coin, wall, new Vector2(pos.X + 15, pos.Y + 15)));
                    break;
            }

        }

        public void Update(GameTime gameTime)
        {
            if(pacmanDead && gameTime.TotalGameTime.TotalSeconds.CompareTo(pacmanDeathTime + 3) > 0)
            {
                RestartGame();
            }
            pacman.Update(walls, pacmanDead);
            foreach (Ghosts ghost in ghosts)
            {
                ghost.Update(walls, ghostColor, pacmanDead);
                ghostColor++;
                if (ghostColor > 3)
                {
                    ghostColor = 1;
                }
                if (!pacmanDead)
                {
	                if(ghost.Hit(pacman.PosRect))
	                {
	                    KillPacman(gameTime);
	                }
                }
            }
            foreach (Coins coin in coins)
            {
                coin.Hit(pacman.PosRect);
            }
            lifeSource();
            Console.WriteLine(numberOfLives);
        }

        private void RestartGame()
        {
            pacmanDead = false;
            pacman.Respawn(pacmanRespawnPos);
            GhostsRespawn(ghostRespawnPos);    
        }

        private void GhostsRespawn(Vector2 ghostRespawnPos)
        {
            foreach (Ghosts ghost in ghosts)
            {
                ghost.Respawn(ghostRespawnPos);
            }
        }

        private void KillPacman(GameTime gameTime)
        {
            pacmanDeathTime = gameTime.TotalGameTime.TotalSeconds;
            numberOfLives--;
            if(numberOfLives==0)
            {
                EndOfGame();
            }
            else
            {
                pacmanDead = true;
            }
        }

        private void EndOfGame()
        {
            game.EndGame();
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
            Rectangle lifeRect = new Rectangle(PacManGame.TILE_SIZE * 2, PacManGame.TILE_SIZE * 15, PacManGame.TILE_SIZE * (numberOfLives - 1), PacManGame.TILE_SIZE);
            spritebatch.Draw(life, lifeRect, lifeSource(), Color.White);
        }

        private void DrawFonts(SpriteBatch spritebatch)
        {
            Vector2 livesLen = hudfont.MeasureString(lives);
            spritebatch.DrawString(hudfont, lives, new Vector2(PacManGame.TILE_SIZE, PacManGame.TILE_SIZE * 15 + livesLen.Y / 2), Color.White);
        }
        private Rectangle lifeSource()
        {
            Rectangle lifeSrcRect = new Rectangle(0, 0, life.Width /2 * (numberOfLives - 1), life.Height);
            return lifeSrcRect;
        }
    }
}
