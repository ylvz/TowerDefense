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
        List<Moose> mooseList;
        List<Wolf> wolfList;
        List<HedgeHog> hogList;
        Vector2 wolfButtonPos, mooseButtonPos, hogButtonPos;
        Rectangle wolfRect, mooseRect, hogRect;
        //LaserManager laserManager;
        public AnimalManager()
        {
            mooseList = new List<Moose>();
            wolfList = new List<Wolf>();
            hogList=new List<HedgeHog>();
            wolfButtonPos = new Vector2(530, 680);
            mooseButtonPos = new Vector2(680, 680);
            hogButtonPos = new Vector2(380, 680);
            wolfRect = new Rectangle((int)wolfButtonPos.X, (int)wolfButtonPos.Y, TextureHandler.startButton.Width, TextureHandler.startButton.Height);
            mooseRect = new Rectangle((int)mooseButtonPos.X, (int)mooseButtonPos.Y, TextureHandler.quitButton.Width, TextureHandler.quitButton.Height);
            hogRect = new Rectangle((int)hogButtonPos.X, (int)hogButtonPos.Y, TextureHandler.storyButton.Width, TextureHandler.storyButton.Height);
            //laserManager= new LaserManager();
        }

        enum TypeOfDragon { FireDragon, IceDragon, WindDragon, ElectricDragon }

        public List<Moose> GetMoose()
        {
            return mooseList;
        }
        public List<Wolf> GetWolf()
        {
            return wolfList;
        }
        public List<HedgeHog> GetHog()
        {
            return hogList;
        }

        public void AddAnimal(Vector2 pos, EnemyManager enemyManager, string animalType)
        {
            LaserBeam lb = new LaserBeam(pos);
            if (animalType == "Moose")
            {
                Moose moose = new Moose(pos);
                moose.AddLaser(lb);
                mooseList.Add(moose);
            }
            else if (animalType == "Wolf")
            {
                Wolf wolf = new Wolf(pos);
                wolf.AddLaser(lb);
                wolfList.Add(wolf);
            }
            else if (animalType == "HedgeHog")
            {
                HedgeHog hog = new HedgeHog(pos);
                hog.AddLaser(lb);
                hogList.Add(hog);
            }
        }


        public void Update(GameTime gameTime)
        {
            foreach (Moose m in mooseList)
            {
                m.Update(gameTime);
            }
            foreach (Wolf w in wolfList)
            {
                w.Update(gameTime);
            }
            foreach (HedgeHog h in hogList)
            {
                h.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (Moose moose in mooseList)
            {
                moose.Draw(spriteBatch);
            }
            foreach (Wolf wolf in wolfList)
            {
                wolf.Draw(spriteBatch);
            }
            foreach (HedgeHog hog in hogList)
            {
                hog.Draw(spriteBatch);
            }
            spriteBatch.Draw(TextureHandler.wolfButton, wolfButtonPos, Color.White);
            spriteBatch.Draw(TextureHandler.mooseButton, mooseButtonPos, Color.White);
            spriteBatch.Draw(TextureHandler.hogButton, hogButtonPos, Color.White);
            spriteBatch.End();
        }

    }
}
