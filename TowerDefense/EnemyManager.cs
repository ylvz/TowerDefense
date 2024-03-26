using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefense;

namespace TowerDefense
{
    internal class EnemyManager
    {
        public List<WeakEnemy> enemies;
        GraphicsDevice gd;
        Rectangle rect;
        int timeSinceLastCar = 0;
        int millisecondsBetweenCreation = 2000;
        int nrOfCarsInCurrentWave = 5;
        bool isHit = false;
        bool isDead = false;


        public EnemyManager(GraphicsDevice gd)
        {
            enemies = new List<WeakEnemy>();
            this.gd = gd;

        }

        public void LoadWave(GameTime gameTime)
        {
            timeSinceLastCar += gameTime.ElapsedGameTime.Milliseconds;
            if (nrOfCarsInCurrentWave > 0 && timeSinceLastCar > millisecondsBetweenCreation)
            {
                timeSinceLastCar -= millisecondsBetweenCreation;
                WeakEnemy enemy = new WeakEnemy(gd);
                enemies.Add(enemy);
                --nrOfCarsInCurrentWave;
            }
        }

        public void Update(GameTime gameTime)
        {
            LoadWave(gameTime);
            foreach (WeakEnemy enemy in enemies.ToList()) // Use ToList() to create a copy of the list
            {
                enemy.Update(gameTime);
                if (enemy.isDead)
                {
                    enemies.Remove(enemy); // Remove dead enemies from the list
                }
            }



        }

        public void CollisionDetection(List<LaserBeam> lasers, GameTime gameTime, Forest forest)
        {
            foreach (WeakEnemy enemy in enemies)
            {
                if (enemy.hitBox.Intersects(forest.hitBox) && !enemy.hasCollidedWithForest)
                {
                    // Deduct only one life if the enemy has not collided with the forest before
                    forest.maxLife -= 1;
                    enemy.hasCollidedWithForest = true; // Set the flag to true
                }
                
            }

            foreach (LaserBeam lb in lasers)
            {
                foreach (WeakEnemy enemy in enemies)
                {
                    // Perform collision detection with each enemy and laser
                    if (enemy.hitBox.Intersects(lb.hitBox))
                    {
                        if (enemy.cooldownTimer <= 0) // Check if the enemy is not on cooldown
                        {
                            enemy.maxLives -= 1;
                            Debug.WriteLine(enemy.maxLives);
                            enemy.cooldownTimer = enemy.cooldownDuration; // Start the cooldown timer
                            lb.hasHit = true;
                        }
                    }
                    else
                    {
                        enemy.isHit = false;
                    }
                }
            }

            // Cooldown timer update
            foreach (WeakEnemy enemy in enemies)
            {
                if (enemy.cooldownTimer > 0)
                {
                    enemy.cooldownTimer -= gameTime.ElapsedGameTime.Milliseconds;
                }
            }
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (WeakEnemy enemy in enemies)
            {
                 enemy.Draw(spriteBatch);
            }
        }

        public List<Vector2> GetEnemyPositions()
        {
            List<Vector2> positions = new List<Vector2>();
            foreach (WeakEnemy enemy in enemies)
            {
                positions.Add(enemy.GetPosition());
            }
            return positions;
        }



    }
}
