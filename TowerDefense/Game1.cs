using CatmullRom;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();

            Window.Title = "SpeedScan Enforcer";

            base.Initialize();
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureHandler.LoadTextures(Content, GraphicsDevice);
            levelManager = new LevelManager();
            levelManager.CreateLevel(GraphicsDevice);
            carManager = new EnemyManager(GraphicsDevice);
            policeManager = new AnimalManager();
            policeManager.AddPolice(new Vector2(800, 100));
            policeManager.AddPolice(new Vector2(800, 500));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            carManager.Update(gameTime);
            policeManager.Update(gameTime);
            for (int i = 0; i < policeManager.GetAllOfficers().Count; i++)
            {
                carManager.CollisionDetection(policeManager.GetAllOfficers()[i].laser);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            levelManager.Draw(_spriteBatch, 1);
            carManager.Draw(_spriteBatch);
            policeManager.Draw(_spriteBatch);

            base.Draw(gameTime);
        }


    }
}