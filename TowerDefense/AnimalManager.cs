using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence;
using TowerDefense;
using Microsoft.Xna.Framework.Input;

namespace TowerDefense
{
    internal class AnimalManager
    {
        List<Moose> mooseList;
        List<Wolf> wolfList;
        List<HedgeHog> hogList;
        Vector2 wolfButtonPos, mooseButtonPos, hogButtonPos;
        Rectangle wolfRect, mooseRect, hogRect;
        Vector2 animalPos;
        public Animal[] AnimalToBePlaced = new Animal[1];
        public bool IsPlacingObject { get; set; }
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
                Moose moose = new Moose(pos, TextureHandler.mooseTex);
                if (Game1.CanPlaceObject(moose))
                {
                    moose.AddLaser(lb);
                    mooseList.Add(moose);
                }
            }
            else if (animalType == "Wolf")
            {
                Wolf wolf = new Wolf(pos, TextureHandler.wolfTex);
                wolf.AddLaser(lb);
                wolfList.Add(wolf);
            }
            else if (animalType == "HedgeHog")
            {
                HedgeHog hog = new HedgeHog(pos, TextureHandler.hogTex);
                hog.AddLaser(lb);
                hogList.Add(hog);
            }
        }

        public Animal AddPotentialAnimal(int MouseCoordinateX, int MouseCoordinateY)
        {
            Vector2 Position = new Vector2(MouseCoordinateX, MouseCoordinateY);
            return new Moose(Position, TextureHandler.mooseTex);
        }


        public void Update(GameTime gameTime,EnemyManager enemyManager, MouseState currentMouseState)
        {
            //AddAnimal(new Vector2(800, 100), enemyManager, "Wolf");
            //AddAnimal(new Vector2(800, 400), enemyManager, "HedgeHog");
            int MouseCoordinateX = currentMouseState.X;
            int MouseCoordinateY = currentMouseState.Y;
            KeyboardState keyboardState = Keyboard.GetState();

            // Check if the 'M' key is pressed
            if (keyboardState.IsKeyDown(Keys.M))
            {
                // Toggle the IsPlacingObject property
                IsPlacingObject = !IsPlacingObject;

                // If placing object, add a potential animal to be placed
                if (IsPlacingObject)
                {
                    AnimalToBePlaced[0] = AddPotentialAnimal(MouseCoordinateX, MouseCoordinateY);
                }
                else if(KeyMouseReader.LeftClick()&&IsPlacingObject)
                {
                    // If not placing object, add the animal to the list
                    AddAnimal(new Vector2(MouseCoordinateX, MouseCoordinateY), enemyManager, "Moose");
                    // Clear the potential animal
                    AnimalToBePlaced[0] = null;
                }
            }


            //if (KeyMouseReader.LeftClick() && IsPlacingObject && Game1.CanPlaceObject(TowerToBePlaced[0]))
            //{
            //    AddAnimal(new Vector2(MouseCoordinateX, MouseCoordinateY), enemyManager, "Moose");
            //    TowerToBePlaced[0].TowerID = ++Globals.IDTracker;
            //    Globals.Towers.Add(TowerToBePlaced[0]);
            //    TowerToBePlaced[0] = CreateTower(MouseCoordinateX, MouseCoordinateY, WeaponTypes.MachineGun);
            //}
            //else if (TowerToBePlaced[0] != null)
            //{
            //    UpdateTower(MouseCoordinateX, MouseCoordinateY);
            //}
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
            if (IsPlacingObject)
            {
                spriteBatch.Draw(AnimalToBePlaced[0].tex, AnimalToBePlaced[0].pos, Color.White);
            }

            spriteBatch.Draw(TextureHandler.wolfButton, wolfButtonPos, Color.White);
            spriteBatch.Draw(TextureHandler.mooseButton, mooseButtonPos, Color.White);
            spriteBatch.Draw(TextureHandler.hogButton, hogButtonPos, Color.White);
            spriteBatch.End();
        }

    }
}
