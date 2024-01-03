using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TowerDefense
{
    internal class LevelManager
    {

        List<Level> levels;
        public LevelManager()
        {
            levels = new List<Level>();
        }

        public void CreateLevel(GraphicsDevice gd)
        {
            levels.Add(new Level(gd));
        }

        public void Update(GameTime gameTime, int level)
        {
            levels[level - 1].Update(gameTime);

        }

        public void Draw(SpriteBatch sb, int level)
        {
            levels[level - 1].Draw(sb);
        }


    }
}
