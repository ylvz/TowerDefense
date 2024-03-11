using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        List<Enemy> cars;
        GraphicsDevice gd;
        int timeSinceLastCar = 0;
        int millisecondsBetweenCreation = 700;
        int nrOfCarsInCurrentWave = 5;
        bool isHit = false;
        bool isDead = false;

        public EnemyManager(GraphicsDevice gd)
        {
            cars = new List<Enemy>();
            this.gd = gd;
        }

        public void LoadWave(GameTime gameTime)
        {
            timeSinceLastCar += gameTime.ElapsedGameTime.Milliseconds;
            if (nrOfCarsInCurrentWave > 0 && timeSinceLastCar > millisecondsBetweenCreation)
            {
                timeSinceLastCar -= millisecondsBetweenCreation;
                Enemy c = new Enemy(gd);
                cars.Add(c);
                --nrOfCarsInCurrentWave;
            }
        }

        public void Update(GameTime gameTime)
        {
            LoadWave(gameTime);
            foreach (Enemy c in cars)
            {
                c.Update(gameTime);
            }
        }

        public void CollisionDetection(LaserBeam lb)
        {
            foreach (Enemy c in cars)
            {
                if (c.hitBox.Intersects(lb.hitBox))
                {
                    c.maxLives = -1;
                    c.isHit = true;
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy c in cars)
            {
                if (!c.isHit)
                    c.Draw(spriteBatch);
            }
        }
    }
}
