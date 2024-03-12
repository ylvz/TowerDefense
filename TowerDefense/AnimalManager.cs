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
        List<Moose> animalList;
        Vector2 wolfButtonPos, mooseButtonPos, hogButtonPos;
        Rectangle wolfRect, mooseRect, hogRect;
        //LaserManager laserManager;
        public AnimalManager()
        {
            animalList = new List<Moose>();
            wolfButtonPos = new Vector2(530, 680);
            mooseButtonPos = new Vector2(680, 680);
            hogButtonPos = new Vector2(380, 680);
            wolfRect = new Rectangle((int)wolfButtonPos.X, (int)wolfButtonPos.Y, TextureHandler.startButton.Width, TextureHandler.startButton.Height);
            mooseRect = new Rectangle((int)mooseButtonPos.X, (int)mooseButtonPos.Y, TextureHandler.quitButton.Width, TextureHandler.quitButton.Height);
            hogRect = new Rectangle((int)hogButtonPos.X, (int)hogButtonPos.Y, TextureHandler.storyButton.Width, TextureHandler.storyButton.Height);
            //laserManager= new LaserManager();
        }

        enum TypeOfDragon { FireDragon, IceDragon, WindDragon, ElectricDragon }

        public List<Moose> GetAllOfficers()
        {
            return animalList;
        }
        public void AddPolice(Vector2 pos, EnemyManager enemyManager)
        {
            LaserBeam lb = new LaserBeam(pos);
            Moose p = new Moose(pos);
            //IceDragon i = new IceDragon(pos);
            //WindDragon w=new WindDragon(pos);
            //ElectricDragon e=new ElectricDragon(pos);
            p.AddLaser(lb);
            animalList.Add(p);
            //dragonList.Add(i);
            //dragonList.Add(w);
            //dragonList.Add(e);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Moose p in animalList)
            {
                p.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (Moose p in animalList)
            {
                p.Draw(spriteBatch);
            }
            spriteBatch.Draw(TextureHandler.wolfButton, wolfButtonPos, Color.White);
            spriteBatch.Draw(TextureHandler.mooseButton, mooseButtonPos, Color.White);
            spriteBatch.Draw(TextureHandler.hogButton, hogButtonPos, Color.White);
            spriteBatch.End();
        }
    }
}
