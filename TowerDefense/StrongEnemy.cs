using CatmullRom;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefense;
using System.Diagnostics;

namespace TowerDefense
{
    internal class StrongEnemy:Enemy
    {


        public StrongEnemy(GraphicsDevice gd,Texture2D tex):base(gd,tex)
        {


            hitBox = new Rectangle(0, 0, 50, 50); // Initialize hitbox first
            maxLives = 5;
            UpdatePosition();
        }


        public override void Update(GameTime gameTime)
        {
            // Step our location forward along the curve forward
            curve_curpos += curve_speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdatePosition();
            if (maxLives < 1)
            {
                isDead = true;
            }
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {

            if (curve_curpos < 1 & curve_curpos > 0)
                cpath_moving.DrawMovingObject(curve_curpos, _spriteBatch, tex);


            int segmentWidth = 10;

            // Calculate the position of the health bar above the enemy
            int healthBarX = hitBox.X - TextureHandler.healthTex.Width - 5;
            int healthBarY = hitBox.Y - TextureHandler.healthTex.Height - 28;

            if (curve_curpos < 1 & curve_curpos > 0)
            {
                _spriteBatch.Begin();
                for (int i = 0; i < maxLives; i++)
                {
                    Rectangle segmentRect = new Rectangle(healthBarX + i * segmentWidth, healthBarY, segmentWidth, TextureHandler.healthTex.Height);
                    _spriteBatch.Draw(TextureHandler.healthTex, segmentRect, Color.Red);
                }
                _spriteBatch.End();

            }
        }


    }
}
