using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan.GameObjects
{
    class PacMans:GameObject
    {
        //Texture2D texture;
        //Vector2 pos;
        public PacMans(Texture2D texture, Vector2 pos) : base(texture, pos)
        {
            this.pos = pos;
        }

        // Override?
        public void Update()
        {
            PacMovement();
        }

        private void PacMovement()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                pos.X += 1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                pos.X -= 1f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                pos.Y -= 1f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {

                pos.Y += 1f;

            }
            
        }
    }
}
