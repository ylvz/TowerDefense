using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ParticleEngine2D
{
    public class Particle
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public float Lifespan { get; set; }
        public Color Color { get; set; }

        public Particle(Vector2 position, Vector2 velocity, Vector2 acceleration, float lifespan, Color color)
        {
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
            Lifespan = lifespan;
            Color = color;
        }

        public void Update(float deltaTime)
        {
            Velocity += Acceleration * deltaTime;
            Position += Velocity * deltaTime;
            Lifespan -= deltaTime;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            // Draw the particle using spriteBatch.Draw method
            spriteBatch.Draw(texture, Position, null, Color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
