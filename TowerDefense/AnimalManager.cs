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
using System.Data.SqlTypes;

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
        private Vector2 pricePos1, pricePos2, pricePos3;
        private Vector2 moneyPos;
        public static int money;
        public Animal[] AnimalToBePlaced = new Animal[1];
        MouseState previousMouseState;
        public bool IsPlacingObject { get; set; }
        //LaserManager laserManager;
        public AnimalManager()
        {
            mooseList = new List<Moose>();
            wolfList = new List<Wolf>();
            hogList = new List<HedgeHog>();
            wolfButtonPos = new Vector2(530, 680);
            mooseButtonPos = new Vector2(680, 680);
            hogButtonPos = new Vector2(380, 680);
            wolfRect = new Rectangle((int)wolfButtonPos.X, (int)wolfButtonPos.Y, TextureHandler.wolfButton.Width, TextureHandler.wolfButton.Height);
            mooseRect = new Rectangle((int)mooseButtonPos.X, (int)mooseButtonPos.Y, TextureHandler.mooseButton.Width, TextureHandler.mooseButton.Height);
            hogRect = new Rectangle((int)hogButtonPos.X, (int)hogButtonPos.Y, TextureHandler.hogButton.Width, TextureHandler.hogButton.Height);
            pricePos1 = new Vector2(400, 650);
            pricePos2 = new Vector2(550, 650);
            pricePos3 = new Vector2(700, 650);
            moneyPos = new Vector2(960, 50);
            money = 1000;
            //laserManager= new LaserManager();
        }

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

        public List<LaserBeam> GetAllLasers()
        {
            List<LaserBeam> allLasers = new List<LaserBeam>();

            foreach (Animal animal in mooseList.Concat<Animal>(wolfList).Concat<Animal>(hogList))
            {
                allLasers.AddRange(animal.lasers);
            }

            return allLasers;
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
                    money -= 300;
                }
            }
            else if (animalType == "Wolf")
            {
                Wolf wolf = new Wolf(pos, TextureHandler.wolfTex);
                if (Game1.CanPlaceObject(wolf))
                {
                    wolf.AddLaser(lb);
                    wolfList.Add(wolf);
                    money-= 200;
                }
            }
            else if (animalType == "HedgeHog")
            {
                HedgeHog hog = new HedgeHog(pos, TextureHandler.hogTex);
                if (Game1.CanPlaceObject(hog))
                {
                    hog.AddLaser(lb);
                    hogList.Add(hog);
                    money -= 100;
                }
            }
        }

        public Animal AddPotentialMoose(int MouseCoordinateX, int MouseCoordinateY)
        {
            Vector2 Position = new Vector2(MouseCoordinateX, MouseCoordinateY);
            return new Moose(Position, TextureHandler.placingMooseTex);
        }

        public Animal AddPotentialWolf(int MouseCoordinateX, int MouseCoordinateY)
        {
            Vector2 Position = new Vector2(MouseCoordinateX, MouseCoordinateY);
            return new Wolf(Position, TextureHandler.placingWolfTex);
        }

        public Animal AddPotentialHedgeHog(int MouseCoordinateX, int MouseCoordinateY)
        {
            Vector2 Position = new Vector2(MouseCoordinateX, MouseCoordinateY);
            return new HedgeHog(Position, TextureHandler.placingHogTex);
        }


        public void Update(GameTime gameTime, EnemyManager enemyManager, MouseState previousMouseState)
        {
            MouseState currentMouseState = Mouse.GetState();
            int MouseCoordinateX = currentMouseState.X;
            int MouseCoordinateY = currentMouseState.Y;
            KeyboardState keyboardState = Keyboard.GetState();

            // Check if the 'M' key is pressed to start placing a moose
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released && mooseRect.Contains(MouseCoordinateX, MouseCoordinateY))
            {
                if (money > 0)
                {
                    IsPlacingObject = true;
                    AnimalToBePlaced[0] = AddPotentialMoose(MouseCoordinateX, MouseCoordinateY);
                }
                
            }
            // Check if the 'W' key is pressed to start placing a wolf
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released && wolfRect.Contains(MouseCoordinateX, MouseCoordinateY))
            {
                if (money > 0)
                {
                    IsPlacingObject = true;
                    AnimalToBePlaced[0] = AddPotentialWolf(MouseCoordinateX, MouseCoordinateY);
                }

                

            }
            // Check if the 'H' key is pressed to start placing a hedgehog
            else if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released && hogRect.Contains(MouseCoordinateX, MouseCoordinateY))
            {
                if (money > 0)
                {
                    IsPlacingObject = true;
                    AnimalToBePlaced[0] = AddPotentialHedgeHog(MouseCoordinateX, MouseCoordinateY);
                }
                
                
            }

            // Check if the right mouse button is clicked to place the animal
            if (IsPlacingObject && currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released&&money>0)
            {
                if (AnimalToBePlaced[0] is Moose)
                {   
                        AddAnimal(new Vector2(MouseCoordinateX, MouseCoordinateY), enemyManager, "Moose");

                }
                if (AnimalToBePlaced[0] is Wolf)
                {
                    AddAnimal(new Vector2(MouseCoordinateX, MouseCoordinateY), enemyManager, "Wolf");

                }
                if (AnimalToBePlaced[0] is HedgeHog)
                {
                    AddAnimal(new Vector2(MouseCoordinateX, MouseCoordinateY), enemyManager, "HedgeHog");

                }
                IsPlacingObject = false; // Reset placing state
                AnimalToBePlaced[0] = null; // Clear potential animal
            }

            // Update the potential animal's position to follow the mouse
            if (IsPlacingObject && AnimalToBePlaced[0] != null)
            {
                AnimalToBePlaced[0].pos = new Vector2(MouseCoordinateX, MouseCoordinateY);
            }

            // Update previous mouse state
            previousMouseState = currentMouseState;

            // Update animals
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
            if (IsPlacingObject && AnimalToBePlaced[0] != null)
            {
                spriteBatch.Draw(AnimalToBePlaced[0].tex, AnimalToBePlaced[0].pos, Color.White);
            }

            spriteBatch.Draw(TextureHandler.wolfButton, wolfButtonPos, Color.White);
            spriteBatch.Draw(TextureHandler.mooseButton, mooseButtonPos, Color.White);
            spriteBatch.Draw(TextureHandler.hogButton, hogButtonPos, Color.White);
            spriteBatch.Draw(TextureHandler.priceTex1, pricePos1, Color.White);
            spriteBatch.Draw(TextureHandler.priceTex2, pricePos2, Color.White);
            spriteBatch.Draw(TextureHandler.priceTex3, pricePos3, Color.White);
            spriteBatch.DrawString(TextureHandler.spriteFont, "Credit: " + money + " $", moneyPos, Color.White);
            spriteBatch.End();
        }

    }
}