using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan.GameObjects.StillObjects
{
   class PointFloor : GameObject
    {
        private int points;
        private AddPointsDelegate addPointsDelegate;
        private bool pointsAwarded;
        Texture2D pointsAwardedTexture;

        public PointFloor(Texture2D texture, Texture2D pointsAwardedtexture, Vector2 pos, AddPointsDelegate addPointsDelegate, int points) : base(texture, pos)
        {
            this.pointsAwardedTexture = pointsAwardedtexture;
            this.addPointsDelegate = addPointsDelegate;
            this.points = points;
            pointsAwarded = false;
        }

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)pos.X, (int)pos.Y, PacManGame.TILE_SIZE, PacManGame.TILE_SIZE);
            }
        }
        public delegate void AddPointsDelegate(int awardPoints);

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!pointsAwarded)
            {
                spriteBatch.Draw(texture, Rect, Color.White);
            }
            else
            {
                spriteBatch.Draw(pointsAwardedTexture, Rect, Color.White);
            }
        }
        public bool Hit(Rectangle hitRect)
        {
            if (Rect.Intersects(hitRect))
            {
                if (!pointsAwarded)
                {
                    addPointsDelegate(points);
                    pointsAwarded = true;
                }
                return true;
            }
            return false;
        }
    }
}
