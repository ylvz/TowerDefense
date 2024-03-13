using CatmullRom;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using TowerDefense;

namespace TowerDefense
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        LevelManager levelManager;
        EnemyManager enemyManager;
        AnimalManager animalManager;
        ButtonManager buttonManager;
        Forest forest;
        KeyboardState ks;
        GameState currentGameState = GameState.Level1;
        RenderTarget2D renderTarget;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1148;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();

            Window.Title = "Forest Defence";

            base.Initialize();
        }

        enum GameState { MainMenu, Level1, WinScreen, LooseScreen }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureHandler.LoadTextures(Content, GraphicsDevice);
            levelManager = new LevelManager();
            levelManager.CreateLevel(GraphicsDevice);
            enemyManager = new EnemyManager(GraphicsDevice);
            animalManager = new AnimalManager();
            buttonManager = new ButtonManager();
            forest = new Forest();
            //animalManager.AddAnimal(new Vector2(800, 500), enemyManager, "Moose");
            //animalManager.AddAnimal(new Vector2(800, 100), enemyManager, "Wolf");
            //animalManager.AddAnimal(new Vector2(800, 400), enemyManager, "HedgeHog");
            renderTarget = new RenderTarget2D(GraphicsDevice, Window.ClientBounds.Width, Window.ClientBounds.Height);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ks = Keyboard.GetState();
            // Debug: Check if the keys are being pressed
            if (ks.IsKeyDown(Keys.W))
                Console.WriteLine("W key pressed");
            if (ks.IsKeyDown(Keys.M))
                Console.WriteLine("M key pressed");
            if (ks.IsKeyDown(Keys.H))
                Console.WriteLine("H key pressed");

            switch (currentGameState)
            {
                case GameState.MainMenu:
                    if (ks.IsKeyDown(Keys.Space))
                    {
                        currentGameState = GameState.Level1;
                    }
                    buttonManager.Update(gameTime, this);
                    break;

                case GameState.Level1:
                    {
                        List<Vector2> enemyPositions = enemyManager.GetEnemyPositions();
                        enemyManager.Update(gameTime);
                        animalManager.Update(gameTime);
                        // Placing animals based on keyboard input
                        if (ks.IsKeyDown(Keys.W)) // Placera en varg (wolf)
                        {
                            Vector2 mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                            Console.WriteLine("Mouse position: " + mousePosition); // Debug: Check mouse position
                            if (CanPlace(mousePosition, TextureHandler.wolfButton.Width, TextureHandler.wolfButton.Height))
                            {
                                animalManager.AddAnimal(mousePosition, enemyManager, "Wolf");
                            }
                        }
                        else if (ks.IsKeyDown(Keys.M)) // Placera en älg (moose)
                        {
                            Vector2 mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                            Console.WriteLine("Mouse position: " + mousePosition); // Debug: Check mouse position
                            if (CanPlace(mousePosition, TextureHandler.mooseButton.Width, TextureHandler.mooseButton.Height))
                            {
                                animalManager.AddAnimal(mousePosition, enemyManager, "Moose");
                            }
                        }
                        else if (ks.IsKeyDown(Keys.H)) // Placera en igelkott (hedgehog)
                        {
                            Vector2 mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                            Console.WriteLine("Mouse position: " + mousePosition); // Debug: Check mouse position
                            if (CanPlace(mousePosition, TextureHandler.hogButton.Width, TextureHandler.hogButton.Height))
                            {
                                animalManager.AddAnimal(mousePosition, enemyManager, "HedgeHog");
                            }
                        }

                        base.Update(gameTime);
                    }
                    break;
            }

        }

        protected override void Draw(GameTime gameTime)
        {

            switch (currentGameState)
            {
                case GameState.MainMenu:
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(TextureHandler.mainMenu, GraphicsDevice.Viewport.Bounds, Color.White);
                    buttonManager.Draw(_spriteBatch);
                    _spriteBatch.End();


                    break;

                case GameState.Level1:
                    GraphicsDevice.SetRenderTarget(renderTarget);
                    GraphicsDevice.Clear(Color.Transparent);

                    levelManager.Draw(_spriteBatch, 1);
                    enemyManager.Draw(_spriteBatch);
                    animalManager.Draw(_spriteBatch);
                    forest.Draw(_spriteBatch);
                    base.Draw(gameTime);
                    GraphicsDevice.SetRenderTarget(null);
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
                    _spriteBatch.End();
                    break;

            }


        }

        public void SwitchToLevel1()
        {
            currentGameState = GameState.Level1;
        }
        public void SwitchToStory()
        {
            currentGameState = GameState.Level1;
        }

        private bool CanPlace(Vector2 position, int width, int height)
        {
            // Skapa en rektangel baserat på objektets position och dimensioner
            Rectangle objRect = new Rectangle((int)position.X, (int)position.Y, width, height);

            // Kolla varje pixelfärg i carpath-texturen och se om någon av dem är opak
            Color[] carpathColors = new Color[TextureHandler.texture_road.Width * TextureHandler.texture_road.Height];
            TextureHandler.texture_road.GetData(carpathColors);
            foreach (Color color in carpathColors)
            {
                if (color.A > 0)
                {
                    // Om det finns minst en opak pixel, objektet kan inte placeras där
                    return false;
                }
            }

            // Om ingen opak pixel hittades, objektet kan placeras där
            return true;
        }




    }
}