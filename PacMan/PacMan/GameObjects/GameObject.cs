using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan.GameObjects
{
    class GameObject
    {        
        public Texture2D texture;
        private Vector2 pos;
        private Rectangle posRect;

        public GameObject(Texture2D texture, Vector2 pos)
        {
            this.texture = texture;
            Pos = pos;
        }

        public Vector2 Pos
        {
            get
            {
                return pos;
            }

            set
            {
                pos = value;
                SetPosRect();
            }
        }

        public Rectangle PosRect
        {
            get
            {
                return posRect;
            }
            set
            {
                posRect = value;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, PosRect, Color.White);
        }

        public void SetPosX(float value)
        {
            pos.X = value;
            SetPosRect();
        }

        public virtual void SetPosRect()
        {
            posRect = new Rectangle((int)Pos.X, (int)Pos.Y, PacManGame.TILE_SIZE, PacManGame.TILE_SIZE);
        }

    }
}
