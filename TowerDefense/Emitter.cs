using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleEngine2D
{
    public class Emitter
    {
        private List<Particle> particles;
        private Texture2D texture;
        private Random random;

        public Emitter(Texture2D texture)
        {
            particles = new List<Particle>();
            this.texture = texture;
            random = new Random();
        }

        public void Update(float deltaTime)
        {
            // Update existing particles
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                particles[i].Update(deltaTime);
                if (particles[i].Lifespan <= 0)
                {
                    particles.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw all particles
            spriteBatch.Begin();
            foreach (var particle in particles)
            {
                particle.Draw(spriteBatch, texture);
            }
            spriteBatch.End();
        }

        public void Emit(Vector2 position, int count)
{
    // Create and add new particles to the list
    for (int i = 0; i < count; i++)
    {
        
        Vector2 offset = new Vector2(random.Next(-10, 10), random.Next(20, 30)); 

        // Apply the offset to the enemy's position
        Vector2 newPosition = position + offset;

        
        Vector2 velocity = new Vector2((float)(random.NextDouble() * 2 - 1), (float)(random.NextDouble() * 2 - 1));
        Vector2 acceleration = new Vector2(0, 0.1f);
        float lifespan = 1.0f;
                Color color = Color.White;

        particles.Add(new Particle(newPosition, velocity, acceleration, lifespan, color));
    }
}

    }
}