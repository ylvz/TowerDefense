using CatmullRom;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

            Window.Title = "SpeedScan Enforcer";

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
            animalManager.AddPolice(new Vector2(800, 100),enemyManager);
            animalManager.AddPolice(new Vector2(800, 500), enemyManager);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (currentGameState)
            {
                case GameState.MainMenu:
                    ks = Keyboard.GetState();
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
                        for (int i = 0; i < animalManager.GetAllOfficers().Count; i++)
                        {
                            enemyManager.CollisionDetection(animalManager.GetAllOfficers()[i].laser,gameTime,forest);
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
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    
                    levelManager.Draw(_spriteBatch, 1);
                    enemyManager.Draw(_spriteBatch);
                    animalManager.Draw(_spriteBatch);
                    forest.Draw(_spriteBatch);
                    base.Draw(gameTime);
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


    }
}