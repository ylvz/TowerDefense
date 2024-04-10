using CatmullRom;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace TowerDefense
{
    internal class WeakEnemy:Enemy
    {

        public WeakEnemy(GraphicsDevice gd,Texture2D tex):base(gd,tex)
        {
            // Initialize hitBox and pos
            hitBox = new Rectangle(0, 0, 50, 50);
            maxLives = 3;
            UpdatePosition();
        }

        public override void Update(GameTime gameTime)
        {
            curve_curpos += curve_speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdatePosition();

            if (maxLives < 1)
            {
                isDead = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (curve_curpos >= 0 && curve_curpos <= 1)
            {
                cpath_moving.DrawMovingObject(curve_curpos, spriteBatch, tex);

                int segmentWidth = 10;
                int healthBarX = hitBox.X - TextureHandler.healthTex.Width - 5;
                int healthBarY = hitBox.Y - TextureHandler.healthTex.Height - 28;

                spriteBatch.Begin();
                for (int i = 0; i < maxLives; i++)
                {
                    Rectangle segmentRect = new Rectangle(healthBarX + i * segmentWidth, healthBarY, segmentWidth, TextureHandler.healthTex.Height);
                    spriteBatch.Draw(TextureHandler.healthTex, segmentRect, Color.Red);
                }
                spriteBatch.End();
            }
        }


    }
}
