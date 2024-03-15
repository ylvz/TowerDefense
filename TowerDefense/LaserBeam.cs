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
    public class LaserBeam
    {
        float range = 40;
        float speed = 3;
        float rotation = 0;
        Vector2 direction;
        Vector2 pos;
        Vector2 enemyPos;
        Vector2 startPos;
        public bool hasHit;
        public Rectangle hitBox;
        private float rotationSpeed = MathHelper.ToRadians(-1);

        public LaserBeam(Vector2 startPos)
        {
            pos = startPos;
            this.startPos = startPos;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, TextureHandler.antlerTex.Height, TextureHandler.antlerTex.Width);


        }

        public void Update(GameTime gameTime)
        {
            pos.X -= speed;
            rotation += rotationSpeed;
            hitBox.X = (int)pos.X;

            if (rotation > MathHelper.TwoPi)
            {
                rotation -= MathHelper.TwoPi;
            }
        }

        public Vector2 GetPos()
        {
            return pos;
        }

        public void Draw(SpriteBatch spriteBatch, string animalType)
        {
            // Calculate the origin point to rotate around
            Vector2 origin = new Vector2(TextureHandler.antlerTex.Width / 2, TextureHandler.antlerTex.Height / 2);

            // Draw with rotation

            if (!hasHit) 
            {
                if (animalType == "Moose")
                {
                    spriteBatch.Draw(TextureHandler.antlerTex, pos, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0.0f);
                }
                if (animalType == "Wolf")
                {
                    spriteBatch.Draw(TextureHandler.biteTex, pos, null, Color.White, 0, origin, 1.0f, SpriteEffects.None, 0.0f);
                }
                if (animalType == "HedgeHog")
                {
                    spriteBatch.Draw(TextureHandler.spikesTex, pos, null, Color.White, 0, origin, 1.0f, SpriteEffects.None, 0.0f);
                }
            }

        }




    }
}
