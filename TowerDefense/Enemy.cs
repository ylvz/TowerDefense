using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatmullRom;

namespace TowerDefense
{
    internal class Enemy
    {
        protected CatmullRomPath cpath_moving;
        protected Texture2D tex;
        protected GraphicsDevice gd;
        protected float curve_curpos = 0;
        protected float curve_speed = 0.03f;
        public int maxLives;
        public bool isHit = false;
        public bool isDead = false;
        public Rectangle hitBox;
        public int cooldownTimer = 0;
        public int cooldownDuration = 1000;
        public bool hasCollidedWithForest = false;
        protected Vector2 pos;

        public Enemy(GraphicsDevice gd, Texture2D tex)
        {
            this.gd = gd;
            this.tex = tex;
            float tension_carpath = 0.5f;
            cpath_moving = new CatmullRomPath(gd, tension_carpath);

            cpath_moving.Clear();
            LoadPath.LoadPathFromFile(cpath_moving, "carpath1.txt");

            cpath_moving.DrawFillSetup(gd, 2, 1, 256);

        }
        public virtual void Update(GameTime gameTime)
        {
        }
        public virtual void Draw(SpriteBatch _spriteBatch)
        {
        }

        public Vector2 GetPosition()
        {
            return pos;
        }

        public void UpdatePosition()
        {
            if (curve_curpos >= 0 && curve_curpos <= 1)
            {
                Vector2 vec = cpath_moving.EvaluateAt(curve_curpos);
                hitBox.X = (int)vec.X;
                hitBox.Y = (int)vec.Y;
                pos = vec;
            }
        }
    }
}
