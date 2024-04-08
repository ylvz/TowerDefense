﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleEngine2D;
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
        private Emitter mudParticleEmitter;


        public EnemyManager(GraphicsDevice gd)
        {
            enemies = new List<WeakEnemy>();
            strongEnemies = new List<StrongEnemy>();
            this.gd = gd;
            mudParticleEmitter = new Emitter(TextureHandler.mudTex);
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
                mudParticleEmitter.Emit(enemy.pos, 1);

                if (enemy.isDead)
                {
                    enemies.Remove(enemy);
                    AnimalManager.money += 50;
                }

            }


            foreach (StrongEnemy enemy in strongEnemies.ToList())
            {
                enemy.Update(gameTime);
                mudParticleEmitter.Emit(enemy.pos, 1);
                if (enemy.isDead)
                {
                    strongEnemies.Remove(enemy);
                    AnimalManager.money += 100;
                }

            }

            mudParticleEmitter.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            if (isFirstWaveSpawned)
                LoadSecondWave(gameTime);
        }

        public void CollisionWithForest(Forest forest)
        {
            foreach (WeakEnemy enemy in enemies)
            {
                // Check if the enemy's cooldown timer is active
                if (enemy.cooldownTimer <= 0)
                {
                    // Check for collision with the forest's hitbox
                    if (enemy.hitBox.Intersects(forest.hitBox))
                    {
                        // Handle the collision (reduce forest's life, start cooldown timer for the enemy)
                        forest.maxLife -= 1;
                        enemy.cooldownTimer = enemy.cooldownDuration; // Start the cooldown timer
                        Debug.WriteLine(forest.maxLife);
                    }
                }
            }

            foreach (StrongEnemy enemy in strongEnemies)
            {
                // Check if the enemy's cooldown timer is active
                if (enemy.cooldownTimer <= 0)
                {
                    // Check for collision with the forest's hitbox
                    if (enemy.hitBox.Intersects(forest.hitBox))
                    {
                        // Handle the collision (reduce forest's life, start cooldown timer for the enemy)
                        forest.maxLife -= 2;
                        enemy.cooldownTimer = enemy.cooldownDuration; // Start the cooldown timer
                        Debug.WriteLine(forest.maxLife);
                    }
                }
            }
        }


        public void CollisionDetection(List<LaserBeam> lasers, GameTime gameTime)
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
                            int damage = (lb.AnimalType == "Moose") ? 3 : ((lb.AnimalType == "Wolf") ? 2 : 1);
                            Debug.WriteLine(enemy.maxLives);
                            enemy.maxLives -= damage;
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
            mudParticleEmitter.Draw(spriteBatch);
        }

        public Vector2 GetNearestEnemyPosition(Vector2 referencePosition)
        {
            Vector2 nearestEnemyPosition = Vector2.Zero;
            float shortestDistanceSquared = float.MaxValue;

            foreach (WeakEnemy enemy in enemies)
            {
                float distanceSquared = Vector2.DistanceSquared(referencePosition, enemy.pos);
                if (distanceSquared < shortestDistanceSquared)
                {
                    shortestDistanceSquared = distanceSquared;
                    nearestEnemyPosition = enemy.pos;
                }
            }

            foreach (StrongEnemy enemy in strongEnemies)
            {
                float distanceSquared = Vector2.DistanceSquared(referencePosition, enemy.pos);
                if (distanceSquared < shortestDistanceSquared)
                {
                    shortestDistanceSquared = distanceSquared;
                    nearestEnemyPosition = enemy.pos;
                }
            }

            return nearestEnemyPosition;
        }

    }
}
