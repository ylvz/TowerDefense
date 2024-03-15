﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TowerDefense
{
    internal class HedgeHog : Animal
    {
        public LaserBeam laser;

        public HedgeHog(Vector2 pos,Texture2D tex) : base(pos,tex)
        {
            this.pos = pos;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 200;
            currentFrame = new Point(0, 0);
            sheetSize = new Point(2, 1);
            frameSize = new Point(43, 25);
            delay = 400;
            timeSinceLast = 0;
        }


        public void Update(GameTime gameTime)
        {

            PlayerAni(gameTime);
            laser.Update(gameTime);

        }
        public void AddLaser(LaserBeam laser)
        {
            this.laser = laser;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureHandler.hogTex, pos, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            laser.Draw(spriteBatch,"HedgeHog");
        }
    }
}
