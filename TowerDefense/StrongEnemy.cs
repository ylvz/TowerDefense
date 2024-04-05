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
    internal class StrongEnemy
    {
        /// Catmull-Rom path
        CatmullRomPath cpath_moving;
        public int cooldownTimer = 0;
        public int cooldownDuration = 1000;
        public bool hasCollidedWithForest = false;
        Texture2D tex;
        public Vector2 pos;
        public Rectangle hitBox;


        // Current location along the curve (car).
        // 0 and 1 is at the first and last control point, respectively.
        float curve_curpos = 0;
        float curve_speed = 0.03f;

        GraphicsDevice gd;
        public int maxLives = 6;
        public bool isHit = false;
        public bool isDead = false;

        public StrongEnemy(GraphicsDevice gd)
        {
            this.gd = gd;
            float tension_carpath = 0.5f; // 0 = sharp turns, 0.5 = moderate turns, 1 = soft turns
            cpath_moving = new CatmullRomPath(gd, tension_carpath);

            cpath_moving.Clear();
            LoadPath.LoadPathFromFile(cpath_moving, "carpath1.txt");

            // DrawFillSetup must be called (once) for every path that uses DrawFill
            // Call again if curve is altered or if window is resized

            cpath_moving.DrawFillSetup(gd, 2, 1, 256);
            tex = TextureHandler.strongEnemyTex;
            hitBox = new Rectangle(0, 0, 50, 50); // Initialize hitbox first
            UpdatePosition();
        }


        public void Update(GameTime gameTime)
        {
            // Step our location forward along the curve forward
            curve_curpos += curve_speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdatePosition();
            if (maxLives < 1)
            {
                isDead = true;
            }
        }

        private void UpdatePosition()
        {
            if (curve_curpos >= 0 && curve_curpos <= 1)
            {
                Vector2 vec = cpath_moving.EvaluateAt(curve_curpos);
                hitBox.X = (int)vec.X;
                hitBox.Y = (int)vec.Y;
                pos = vec;
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {

            //cpath_moving.DrawFill(gd, TextureHandler.texture_red);

            // Draw control points
            //cpath_road.DrawPoints(_spriteBatch, Color.Black, 6);
            //cpath_moving.DrawPoints(_spriteBatch, Color.Blue, 6);
            if (curve_curpos < 1 & curve_curpos > 0)
                cpath_moving.DrawMovingObject(curve_curpos, _spriteBatch, tex);


            int segmentWidth = maxLives;

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

        public Vector2 GetPosition()
        {
            return pos;
        }

    }
}
