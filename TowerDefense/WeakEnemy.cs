using CatmullRom;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace TowerDefense
{
    internal class WeakEnemy
    {
        CatmullRomPath cpath_moving;
        public int cooldownTimer = 0;
        public int cooldownDuration = 1000;
        public bool hasCollidedWithForest = false;
        Texture2D tex;
        public Vector2 pos;
        public Rectangle hitBox;

        float curve_curpos = 0;
        float curve_speed = 0.03f;

        GraphicsDevice gd;
        public int maxLives = 6;
        public bool isHit = false;
        public bool isDead = false;

        public WeakEnemy(GraphicsDevice gd)
        {
            this.gd = gd;
            float tension_carpath = 0.5f;
            cpath_moving = new CatmullRomPath(gd, tension_carpath);

            cpath_moving.Clear();
            LoadPath.LoadPathFromFile(cpath_moving, "carpath1.txt");

            cpath_moving.DrawFillSetup(gd, 2, 1, 256);
            tex = TextureHandler.weakEnemyTex;

            // Initialize hitBox and pos
            hitBox = new Rectangle(0, 0, 50, 50);
            UpdatePosition();
        }

        public void Update(GameTime gameTime)
        {
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

        public void Draw(SpriteBatch spriteBatch)
        {
            if (curve_curpos >= 0 && curve_curpos <= 1)
            {
                cpath_moving.DrawMovingObject(curve_curpos, spriteBatch, tex);

                int segmentWidth = maxLives;
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

        public Vector2 GetPosition()
        {
            return pos;
        }
    }
}
