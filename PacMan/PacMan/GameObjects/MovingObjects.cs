using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PacMan.GameObjects.StillObjects;

namespace PacMan.GameObjects
{
    class MovingObjects : GameObject
    {
        internal enum Direction { UP, DOWN, LEFT, RIGHT}
        internal Direction CurrentState = Direction.RIGHT;

        internal static Random rnd = new Random();
        internal int rndom = 58;

        internal float speed = 2f;

        public MovingObjects(Texture2D texture, Vector2 pos) : base (texture, pos)
        {

        }

        public void NewRndom()
        {
            rndom = rnd.Next(0, 100);
        }

        public int Rndom()
        {
            
            return rndom;
        }

        public void MoveObject(List<Walls> walls)
        {
            if (!TestDirectionChange(walls))
            {
                Vector2 objPos = new Vector2(Pos.X, Pos.Y);

                switch (CurrentState)
                {
                    case Direction.UP:
                        objPos.Y -= speed;
                        break;
                    case Direction.DOWN:
                        objPos.Y += speed;
                        break;
                    case Direction.LEFT:
                        objPos.X -= speed;
                        break;
                    case Direction.RIGHT:
                        objPos.X += speed;
                        break;
                }
                if (!Collision(objPos, walls))
                {
                    Pos = objPos;
                }
            }

        }

        public virtual bool TestDirectionChange(List<Walls> walls)
        {
            return false;
        }

        internal bool Collision(Vector2 objPos, List<Walls> walls)
        {
            Rectangle temp = new Rectangle((int)objPos.X, (int)objPos.Y, PacManGame.TILE_SIZE, PacManGame.TILE_SIZE);
            foreach (Walls wall in walls)
            {
                if (wall.PosRect.Intersects(temp))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
