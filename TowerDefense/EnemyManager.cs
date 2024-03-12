using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefense;

namespace TowerDefense
{
    internal class EnemyManager
    {
        public List<Enemy> enemies;
        GraphicsDevice gd;
        Rectangle rect;
        int timeSinceLastCar = 0;
        int millisecondsBetweenCreation = 700;
        int nrOfCarsInCurrentWave = 5;
        bool isHit = false;
        bool isDead = false;


        public EnemyManager(GraphicsDevice gd)
        {
            enemies = new List<Enemy>();
            this.gd = gd;

        }

        public void LoadWave(GameTime gameTime)
        {
            timeSinceLastCar += gameTime.ElapsedGameTime.Milliseconds;
            if (nrOfCarsInCurrentWave > 0 && timeSinceLastCar > millisecondsBetweenCreation)
            {
                timeSinceLastCar -= millisecondsBetweenCreation;
                Enemy enemy = new Enemy(gd, rect);
                enemies.Add(enemy);
                --nrOfCarsInCurrentWave;
            }
        }

        public void Update(GameTime gameTime)
        {
            LoadWave(gameTime);
            foreach (Enemy enemy in enemies)
            {
                enemy.Update(gameTime);
            }


        }

        public void CollisionDetection(LaserBeam lb, GameTime gameTime, Forest forest)
        {
            foreach (Enemy enemy in enemies)
            {
                // Collision detection with laser beam
                if (enemy.HitBox.Intersects(lb.hitBox))
                {
                    if (enemy.cooldownTimer <= 0) // Check if the enemy is not on cooldown
                    {
                        enemy.maxLives -= 1;
                        enemy.cooldownTimer = enemy.cooldownDuration; // Start the cooldown timer
                        lb.hasHit = true;
                    }
                }
                else
                {
                    enemy.isHit = false;
                }

                // Collision detection with forest
                if (enemy.HitBox.Intersects(forest.hitBox) && !enemy.hasCollidedWithForest)
                {
                    // Deduct only one life if the enemy has not collided with the forest before
                    forest.maxLife -= 1;
                    enemy.hasCollidedWithForest = true; // Set the flag to true
                }
            }

            // Cooldown timer update
            foreach (Enemy enemy in enemies)
            {
                if (enemy.cooldownTimer > 0)
                {
                    enemy.cooldownTimer -= gameTime.ElapsedGameTime.Milliseconds;
                }
            }
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemies)
            {
                if (!enemy.isDead)
                    enemy.Draw(spriteBatch);
            }
        }

        public List<Vector2> GetEnemyPositions()
        {
            List<Vector2> positions = new List<Vector2>();
            foreach (Enemy enemy in enemies)
            {
                positions.Add(enemy.GetPosition());
            }
            return positions;
        }



    }
}
