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
    internal class Moose:Animal
    {
        public LaserBeam laser;
        public Moose(Vector2 pos,Texture2D tex):base(pos,tex)
        {
            currentFrame = new Point(0, 0);
            sheetSize = new Point(8, 1);
            frameSize = new Point(50, 50);
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 200;
            delay = 400;
            timeSinceLast = 0;
        }

        public void AddLaser(LaserBeam laser)
        {
            this.laser = laser;
        }

        public virtual void Update(GameTime gameTime)
        {

            PlayerAni(gameTime);
            laser.Update(gameTime);

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(tex, pos, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            laser.Draw(spriteBatch,"Moose");
        }
    }
}
