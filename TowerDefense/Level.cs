using CatmullRom;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerDefense;

namespace TowerDefense
{
    internal class Level
    {
        /// Catmull-Rom path
        CatmullRomPath cpath_road;
        CatmullRomPath cpath_moving;
        public static int levelNr;

        // Current location along the curve (car).
        // 0 and 1 is at the first and last control point, respectively.
        float curve_curpos = 0;
        // How fast to go along the curve = fraction of curve / second
        float curve_speed = 0.2f;

        GraphicsDevice gd;

        public Level(GraphicsDevice gd)
        {
            levelNr += 1;
            this.gd = gd;
            float tension_road = 0.5f; // 0 = sharp turns, 0.5 = moderate turns, 1 = soft turns
            cpath_road = new CatmullRomPath(gd, tension_road);

            //float tension_carpath = 0.5f; // 0 = sharp turns, 0.5 = moderate turns, 1 = soft turns
            //cpath_moving = new CatmullRomPath(gd, tension_carpath);



            cpath_road.Clear();
            //            LoadPathFromFile(cpath_road, "spiral.txt");
            LoadPath.LoadPathFromFile(cpath_road, "carpath1.txt");

            //cpath_moving.Clear();
            //LoadPathFromFile(cpath_moving, "carpath1.txt");

            // DrawFillSetup must be called (once) for every path that uses DrawFill
            // Call again if curve is altered or if window is resized
            cpath_road.DrawFillSetup(gd, 30, 20, 26);
            //cpath_moving.DrawFillSetup(gd, 2, 1, 256);
        }

        public void Update(GameTime gameTime)
        {
            // Step our location forward along the curve forward
            //curve_curpos += curve_speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //// Reset if we reach the end
            //if (curve_curpos > 1.0f) curve_curpos = 0.0f;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            // Draw filled paths
            _spriteBatch.Begin();
            _spriteBatch.Draw(TextureHandler.background, gd.Viewport.Bounds, Color.White);
            _spriteBatch.End();
            cpath_road.DrawFill(gd, TextureHandler.texture_road);
            //cpath_moving.DrawFill(gd, TextureHandler.texture_red);

            // Draw control points
            //cpath_road.DrawPoints(_spriteBatch, Color.Black, 6);
            //cpath_moving.DrawPoints(_spriteBatch, Color.Blue, 6);

            //cpath_moving.DrawMovingObject(curve_curpos, _spriteBatch, TextureHandler.texture_car);
        }



    }
}
