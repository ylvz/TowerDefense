using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefense;

namespace TowerDefense
{
    internal class AnimalManager
    {
        List<Moose> dragonList;
        //LaserManager laserManager;
        public AnimalManager()
        {
            dragonList = new List<Moose>();
            //laserManager= new LaserManager();
        }

        enum TypeOfDragon { FireDragon, IceDragon, WindDragon, ElectricDragon }

        public List<Moose> GetAllOfficers()
        {
            return dragonList;
        }
        public void AddPolice(Vector2 pos)
        {
            LaserBeam lb = new LaserBeam(pos);
            Moose p = new Moose(pos);
            //IceDragon i = new IceDragon(pos);
            //WindDragon w=new WindDragon(pos);
            //ElectricDragon e=new ElectricDragon(pos);
            p.AddLaser(lb);
            dragonList.Add(p);
            //dragonList.Add(i);
            //dragonList.Add(w);
            //dragonList.Add(e);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Moose p in dragonList)
            {
                p.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (Moose p in dragonList)
            {
                p.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
