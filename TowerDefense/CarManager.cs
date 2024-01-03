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
    internal class CarManager
    {
        List<Car> cars;
        GraphicsDevice gd;
        int timeSinceLastCar = 0;
        int millisecondsBetweenCreation = 700;
        int nrOfCarsInCurrentWave = 5;

        public CarManager(GraphicsDevice gd)
        {
            cars = new List<Car>();
            this.gd = gd;
        }

        public void LoadWave(GameTime gameTime)
        {
            timeSinceLastCar += gameTime.ElapsedGameTime.Milliseconds;
            if (nrOfCarsInCurrentWave > 0 && timeSinceLastCar > millisecondsBetweenCreation)
            {
                timeSinceLastCar -= millisecondsBetweenCreation;
                Car c = new Car(gd);
                cars.Add(c);
                --nrOfCarsInCurrentWave;
            }
        }

        public void Update(GameTime gameTime)
        {
            LoadWave(gameTime);
            foreach (Car c in cars)
            {
                c.Update(gameTime);
            }
        }

        public void CollisionDetection(LaserBeam lb)
        {
            foreach (Car c in cars)
            {
                if (c.hitBox.Intersects(lb.hitBox))
                    c.isHit = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Car c in cars)
            {
                if (!c.isHit)
                    c.Draw(spriteBatch);
            }
        }
    }
}
