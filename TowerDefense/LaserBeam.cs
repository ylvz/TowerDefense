using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefense;

namespace TowerDefense
{
    internal class LaserBeam
    {
        float range = 40;
        float speed = 2;
        Vector2 pos;
        Vector2 startPos;
        public Rectangle hitBox;

        public LaserBeam(Vector2 startPos)
        {
            pos = startPos;
            this.startPos = startPos;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, TextureHandler.texture_yellow.Height, TextureHandler.texture_yellow.Width);
        }

        public void Update(GameTime gameTime)
        {
            pos.X -= speed;
            hitBox.X = (int)pos.X;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureHandler.texture_yellow, pos, Color.White);
        }



    }
}
