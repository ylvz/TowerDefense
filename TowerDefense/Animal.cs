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
        public List<LaserBeam> lasers;
        protected float timeSinceLastShot;
        protected float shotInterval;


        public Animal(Vector2 Pos, Texture2D Tex)
        {
            tex = Tex;
            pos = Pos;
            lasers = new List<LaserBeam>();
        }

        public void AddLaser(LaserBeam laser)
        {
            lasers.Add(laser);
        }

        public void FireShot()
        {
            // Create a new instance of LaserBeam representing the shot
            LaserBeam newShot = new LaserBeam(pos);
            // Add the new shot to the list of lasers
            lasers.Add(newShot);
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
