﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan.GameObjects
{
    class GameObject
    {        
        Texture2D texture;
        public Vector2 pos;

        public GameObject(Texture2D texture, Vector2 pos)
        {
            this.texture = texture;
            this.pos = pos;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, Color.White);
        }

    }
}
