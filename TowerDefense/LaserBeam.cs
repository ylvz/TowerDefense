using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TowerDefense
{
    public class LaserBeam
    {
        // Your existing fields
        private float speed = 0;
        private float rotation = 0;
        Vector2 pos;
        public bool hasHit;
        public Rectangle hitBox;
        private float rotationSpeed = MathHelper.ToRadians(180);
        public string AnimalType { get; set; }


        public LaserBeam(Vector2 startPos,string animalType)
        {
            pos = startPos;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, TextureHandler.antlerTex.Height, TextureHandler.antlerTex.Width);
            AnimalType = animalType;

        }

        public void Update(GameTime gameTime, Vector2 targetPosition, string animalType)
        {
            if (animalType == "Moose")
            {
                Vector2 direction = Vector2.Normalize(targetPosition - pos);
                speed = 100;
                // Update position based on direction and speed
                pos += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Update hitbox position accordingly
                hitBox.X = (int)pos.X;
                hitBox.Y = (int)pos.Y;

                // Update rotation if needed
                // (This code might need modification based on how you handle rotation)
                rotation += rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (rotation > MathHelper.Pi)
                {
                    rotation -= MathHelper.TwoPi;
                }
                else if (rotation < -MathHelper.Pi)
                {
                    rotation += MathHelper.TwoPi;
                }
            }
            if (animalType == "Wolf")
            {
                Vector2 direction = Vector2.Normalize(targetPosition - pos);
                speed = 150;
                // Update position based on direction and speed
                pos += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Update hitbox position accordingly
                hitBox.X = (int)pos.X;
                hitBox.Y = (int)pos.Y;

            }
            if (animalType == "HedgeHog")
            {
                Vector2 direction = Vector2.Normalize(targetPosition - pos);
                speed = 200;
                // Update position based on direction and speed
                pos += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Update hitbox position accordingly
                hitBox.X = (int)pos.X;
                hitBox.Y = (int)pos.Y;

            }
        }

        public void Draw(SpriteBatch spriteBatch, string animalType)
        {
            // Calculate the origin point to rotate around
            Vector2 origin = new Vector2(TextureHandler.antlerTex.Width / 2, TextureHandler.antlerTex.Height / 2);

            // Draw with rotation

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

        public bool IsOutOfBounds()
        {
            return pos.X < 0;
        }
    }
}

