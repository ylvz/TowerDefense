using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    public class LaserBeam
    {
        // Your existing fields
        private float speed = 3;
        private float rotation = 0;
        private Vector2 pos;
        public bool hasHit;
        public Rectangle hitBox;
        private float rotationSpeed = MathHelper.ToRadians(-1);

        public LaserBeam(Vector2 startPos)
        {
            pos = startPos;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, TextureHandler.antlerTex.Height, TextureHandler.antlerTex.Width);
        }

        public void Update(GameTime gameTime)
        {
            // Update the position and rotation
            pos.X -= speed;
            rotation += rotationSpeed;
            hitBox.X = (int)pos.X;

            if (rotation > MathHelper.TwoPi)
            {
                rotation -= MathHelper.TwoPi;
            }
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

