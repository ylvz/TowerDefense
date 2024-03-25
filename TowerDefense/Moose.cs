using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefense;
using static System.Net.Mime.MediaTypeNames;

namespace TowerDefense
{
    internal class Moose: Animal
    {
        public Moose(Vector2 pos,Texture2D tex):base(pos,tex)
        {
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            currentFrame = new Point(0, 0);
            sheetSize = new Point(8, 1);
            frameSize = new Point(50, 50);
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 200;
            delay = 400;
            shotInterval = 5000;
            timeSinceLastShot = 0;
        }

        public Vector2 GetPosition()
        {
            return pos;
        }

        public virtual void Update(GameTime gameTime)
        {
            timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Check if it's time to fire a new shot
            if (timeSinceLastShot >= shotInterval)
            {
                FireShot();
                timeSinceLastShot = 0; // Reset the timer
            }
            foreach (var laser in lasers)
            {
                laser.Update(gameTime);
            }
            PlayerAni(gameTime);

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(TextureHandler.mooseTex, pos, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            foreach (var laser in lasers)
            {
                laser.Draw(spriteBatch, "Moose");
            }
        }
    }
}
