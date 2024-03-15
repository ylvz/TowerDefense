using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    public class Animal
    {
        public Vector2 pos;
        protected int timeSinceLastFrame;
        protected int millisecondsPerFrame;
        protected Point currentFrame;
        protected Point sheetSize;
        protected Point frameSize;
        protected int delay;
        protected int timeSinceLast;
        public Texture2D tex;
        public Rectangle hitBox;


        public Animal(Vector2 Pos, Texture2D Tex)
        {
            tex = Tex;
            pos = Pos;
        }


        public virtual void PlayerAni(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                    {
                        currentFrame.Y = 0;
                    }
                }
            }
        }
    }
}
