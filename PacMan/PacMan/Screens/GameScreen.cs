﻿using Microsoft.Xna.Framework;
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

        Vector2 pos = new Vector2(60, 60);
        Rectangle pacSrcRect;
        Rectangle pacPos;
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
            pacman = new PacMans(pacSprite, pos);
            StreamReader sr = new StreamReader(@"testlevel.txt");
            while (!sr.EndOfStream)
            {
                strings.Add(sr.ReadLine());
            }
            //for (int i = 0; i < strings.Count; i++)
            //{
            //    for (int k = 0; k < strings[i].Length; k++)
            //    {
            //        walls = new Walls(wall, new Vector2(40* i, 40* k));
            //    }
                
            //}

        }

        public void Update(GameTime gameTime)
        {
            pacman.Update();
        }

        //private void PacMovement()
        //{
        //    Vector2 newPacPos = new Vector2(pos.X, pos.Y);
        //    float newRot = rotation;
        //        if (Keyboard.GetState().IsKeyDown(Keys.Right))
        //        {
        //            pacEffects = SpriteEffects.None;
        //            newPacPos.X += 1;
        //            newRot = MathHelper.ToRadians(0);
        //            movement = true;
        //        }
        //        else if (Keyboard.GetState().IsKeyDown(Keys.Left))
        //        {
        //            pacEffects = SpriteEffects.FlipHorizontally;
        //            newPacPos.X -= 1f;
        //            newRot = MathHelper.ToRadians(0);
        //            movement = true;
        //        }
        //        else if (Keyboard.GetState().IsKeyDown(Keys.Up))
        //        {
        //            pacEffects = SpriteEffects.None;
        //            newPacPos.Y -= 1f;
        //            newRot = MathHelper.ToRadians(-90);
        //            movement = true;
        //        }
        //        else if (Keyboard.GetState().IsKeyDown(Keys.Down))
        //        {
        //            pacEffects = SpriteEffects.FlipHorizontally;
        //            newPacPos.Y += 1f;
        //            newRot = MathHelper.ToRadians(-90);
        //            movement = true;
        //        }
        //        else
        //        {
        //            movement = false;
        //            pacAnimation = 1;
        //        }
        //    if (!Collision(newPacPos))
        //    {
        //        pos = newPacPos;
        //        rotation = newRot;
        //    }
        //    else
        //    {

        //    }
        //}

        public void Draw(SpriteBatch spriteBatch)
        {
            pacSrcRect = new Rectangle((pacSprite.Width / 4) * PacAnimation(), 0, pacSprite.Width / 4, pacSprite.Height);
            pacPos = new Rectangle((int)pos.X - 20, (int)pos.Y - 20, 40, 40);
            wallRect = new Rectangle(100, 100, wall.Width * 4, wall.Height * 4);
            //spriteBatch.Draw(pacSprite, pos, pacSrcRect, Color.White, rotation, new Vector2(20, 20), scale, pacEffects, 1f);
            //spriteBatch.Draw(wall, wallRect, Color.White);
            pacman.Draw(spriteBatch);
            for (int i = 0; i < strings.Count; i++)
            {
                for (int k = 0; k < strings[i].Length; k++)
                {
                    if (strings[i][k] == 'W')
                    {
                        spriteBatch.Draw(wall, new Rectangle(40 * i, 40 * k, 40, 40), Color.White);
                    }
                }
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
        private bool Collision(Vector2 newPacPos)
        {
            Rectangle temp = new Rectangle((int)newPacPos.X - 20, (int)newPacPos.Y - 20, 40, 40);
            if (wallRect.Intersects(temp))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
