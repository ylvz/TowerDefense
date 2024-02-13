using CatmullRom;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.Security.Principal;
using TowerDefense;

namespace TowerDefense
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        LevelManager levelManager;
        EnemyManager carManager;
        AnimalManager policeManager;
        ButtonManager buttonManager;
        KeyboardState ks;
        GameState currentGameState = GameState.MainMenu;

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
            carManager = new EnemyManager(GraphicsDevice);
            policeManager = new AnimalManager();
            buttonManager = new ButtonManager();
            policeManager.AddPolice(new Vector2(800, 100));
            policeManager.AddPolice(new Vector2(800, 500));
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
                        carManager.Update(gameTime);
                        policeManager.Update(gameTime);
                        for (int i = 0; i < policeManager.GetAllOfficers().Count; i++)
                        {
                            carManager.CollisionDetection(policeManager.GetAllOfficers()[i].laser);
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
                    carManager.Draw(_spriteBatch);
                    policeManager.Draw(_spriteBatch);

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