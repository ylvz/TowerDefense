using CatmullRom;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefense;

namespace TowerDefense
{
    internal class Car
    {
        /// Catmull-Rom path
        CatmullRomPath cpath_moving;

        public Rectangle hitBox;

        // Current location along the curve (car).
        // 0 and 1 is at the first and last control point, respectively.
        float curve_curpos = 0;
        // How fast to go along the curve = fraction of curve / second
        float curve_speed = 0.08f;

        GraphicsDevice gd;
        public bool isHit = false;

        public Car(GraphicsDevice gd)
        {
            this.gd = gd;

            float tension_carpath = 0.5f; // 0 = sharp turns, 0.5 = moderate turns, 1 = soft turns
            cpath_moving = new CatmullRomPath(gd, tension_carpath);

            cpath_moving.Clear();
            LoadPath.LoadPathFromFile(cpath_moving, "carpath1.txt");

            // DrawFillSetup must be called (once) for every path that uses DrawFill
            // Call again if curve is altered or if window is resized

            cpath_moving.DrawFillSetup(gd, 2, 1, 256);

            hitBox = new Rectangle(0, 0, 50, 50);
        }

        public void Update(GameTime gameTime)
        {
            // Step our location forward along the curve forward

            curve_curpos += curve_speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (curve_curpos < 1 & curve_curpos > 0)
            {
                Vector2 vec = cpath_moving.EvaluateAt(curve_curpos);
                hitBox.X = (int)vec.X;
                hitBox.Y = (int)vec.Y;
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {

            //cpath_moving.DrawFill(gd, TextureHandler.texture_red);

            // Draw control points
            //cpath_road.DrawPoints(_spriteBatch, Color.Black, 6);
            //cpath_moving.DrawPoints(_spriteBatch, Color.Blue, 6);
            if (curve_curpos < 1 & curve_curpos > 0)
                cpath_moving.DrawMovingObject(curve_curpos, _spriteBatch, TextureHandler.texture_car);
        }

    }
}
