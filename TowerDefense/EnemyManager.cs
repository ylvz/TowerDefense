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
        public List<StrongEnemy> strongEnemies;
        GraphicsDevice gd;
        Rectangle rect;
        int timeSinceLastCar = 0;
        int millisecondsBetweenWeakCreation = 1700;
        int millisecondsBetweenStrongCreation = 3500;
        int nrOfWeakEnemiesInCurrentWave = 5; // Track the number of weak enemies
        int nrOfStrongEnemiesInCurrentWave = 5; // Track the number of strong enemies
        bool isHit = false;
        bool isDead = false;
        bool isFirstWaveSpawned = false;

        public EnemyManager(GraphicsDevice gd)
        {
            enemies = new List<WeakEnemy>();
            strongEnemies= new List<StrongEnemy>();
            this.gd = gd;

        }

        public void LoadWave(GameTime gameTime)
        {
            timeSinceLastCar += gameTime.ElapsedGameTime.Milliseconds;

            if (!isFirstWaveSpawned)
            {
                if (nrOfWeakEnemiesInCurrentWave > 0 && timeSinceLastCar > millisecondsBetweenWeakCreation)
                {
                    timeSinceLastCar -= millisecondsBetweenWeakCreation;
                    WeakEnemy enemy = new WeakEnemy(gd);
                    enemies.Add(enemy);
                    --nrOfWeakEnemiesInCurrentWave;
                }
                else if (nrOfWeakEnemiesInCurrentWave == 0)
                {
                    isFirstWaveSpawned = true;
                    timeSinceLastCar = 0; // Reset time since last car for the second wave
                }
            }
            else if (strongEnemies.Count == 0)
            {
                LoadSecondWave(gameTime);
            }
        }


        public void LoadSecondWave(GameTime gameTime)
        {
            // Wait for 5 seconds before spawning the second wave
            if (timeSinceLastCar < 20000)
            {
                timeSinceLastCar += gameTime.ElapsedGameTime.Milliseconds;
                return;
            }

            if (nrOfStrongEnemiesInCurrentWave > 0 && timeSinceLastCar > millisecondsBetweenStrongCreation)
            {
                timeSinceLastCar -= millisecondsBetweenStrongCreation;
                StrongEnemy enemy = new StrongEnemy(gd);
                strongEnemies.Add(enemy);
                --nrOfStrongEnemiesInCurrentWave; // Decrement the number of strong enemies
            }
        }




        public void Update(GameTime gameTime)
        {
            LoadWave(gameTime);

            foreach (WeakEnemy enemy in enemies.ToList())
            {
                enemy.Update(gameTime);
                if (enemy.isDead)
                {
                    enemies.Remove(enemy);
                    Forest.money += 50;
                }

            }

            foreach (StrongEnemy enemy in strongEnemies.ToList())
            {
                enemy.Update(gameTime);
                if (enemy.isDead)
                {
                    Forest.money += 100;
                    strongEnemies.Remove(enemy);
                }

            }

            if (isFirstWaveSpawned)
                LoadSecondWave(gameTime);
        }



        public void CollisionDetection(List<LaserBeam> lasers, GameTime gameTime, Forest forest)
        {
            for (int i = lasers.Count - 1; i >= 0; i--)
            {
                LaserBeam lb = lasers[i];

                // Collision detection with weak enemies
                foreach (WeakEnemy enemy in enemies)
                {
                    if (enemy.hitBox.Intersects(lb.hitBox))
                    {
                        if (enemy.cooldownTimer <= 0) // Check if the enemy is not on cooldown
                        {
                            enemy.maxLives -= 1;
                            Debug.WriteLine(enemy.maxLives);
                            enemy.cooldownTimer = enemy.cooldownDuration; // Start the cooldown timer
                            lb.hasHit = true; // Mark the laser as hit
                            break; // Exit the loop since the laser has hit an enemy
                        }
                    }
                }


                foreach (StrongEnemy senemy in strongEnemies)
                {
                    if (senemy.hitBox.Intersects(lb.hitBox))
                    {
                        if (senemy.cooldownTimer <= 0) // Check if the enemy is not on cooldown
                        {
                            senemy.maxLives -= 1; // Decrease the life of the enemy
                            Debug.WriteLine(senemy.maxLives);
                            senemy.cooldownTimer = senemy.cooldownDuration; // Start the cooldown timer
                            lb.hasHit = true; // Mark the laser as hit
                            break; // Exit the loop since the laser has hit an enemy
                        }
                    }
                }


                // Remove the laser from the list if it has hit an enemy
                List<LaserBeam> lasersCopy = new List<LaserBeam>(lasers);

                // Remove the laser from the list if it has hit an enemy
                foreach (LaserBeam laser in lasersCopy)
                {
                    if (laser.hasHit)
                    {
                        lasers.Remove(laser);
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
            foreach (StrongEnemy enemy in strongEnemies)
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

            foreach (StrongEnemy enemy in strongEnemies)
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
