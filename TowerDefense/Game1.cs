using CatmullRom;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Principal;
using TowerDefence;
using TowerDefense;

namespace TowerDefense
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static RenderTarget2D renderTarget;
        LevelManager levelManager;
        EnemyManager enemyManager;
        AnimalManager animalManager;
        ButtonManager buttonManager;
        Forest forest;
        MouseState previousMouseState;


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
            renderTarget = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
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
                        MouseState currentMouseState = KeyMouseReader.mouseState;
                        animalManager.Update(gameTime, enemyManager, previousMouseState); // Pass previousMouseState here
                        previousMouseState = currentMouseState; // Update previousMouseState for the next frame
                        DrawOnRenderTarget(levelManager.levels[0]);
                        List<Vector2> enemyPositions = enemyManager.GetEnemyPositions();
                        enemyManager.Update(gameTime);
                        for (int i = 0; i < animalManager.GetMoose().Count; i++)
                        {
                            enemyManager.CollisionDetection(animalManager.GetAllLasers(), gameTime, forest);
                        }
                        for (int i = 0; i < animalManager.GetWolf().Count; i++)
                        {
                            enemyManager.CollisionDetection(animalManager.GetAllLasers(), gameTime, forest);
                        }
                        for (int i = 0; i < animalManager.GetHog().Count; i++)
                        {
                            enemyManager.CollisionDetection(animalManager.GetAllLasers(), gameTime, forest);
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

        private void DrawOnRenderTarget(Level lvl)
        {
            //Ändra så att GraphicsDevice ritar mot vårt render target
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            _spriteBatch.Begin();

            //Rita ut texturen. Den ritas nu ut till vårt render target istället
            //för på skärmen.
            lvl.cpath_road.DrawFill(GraphicsDevice, TextureHandler.texture_road);
            foreach (var moose in animalManager.GetMoose())
            {
                moose.Draw(_spriteBatch);
            }
            foreach (var wolf in animalManager.GetWolf())
            {
                wolf.Draw(_spriteBatch);
            }
            foreach (var hog in animalManager.GetHog())
            {
                hog.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            //Sätt GraphicsDevice att åter igen peka på skärmen
            GraphicsDevice.SetRenderTarget(null);
        }

        public static bool CanPlaceObject(Animal g)
        {
            if (g == null || g.tex == null || renderTarget == null)
            {
                // Handle the case where either g, g.tex, or renderTarget is null
                return false;
            }

            Color[] pixels = new Color[g.tex.Width * g.tex.Height];
            Color[] pixels2 = new Color[g.tex.Width * g.tex.Height];

            // Make sure g.tex is not null before accessing its data
            g.tex.GetData<Color>(pixels2);

            // Make sure renderTarget is not null before accessing its data
            renderTarget.GetData(0, g.hitBox, pixels, 0, pixels.Length);

            for (int i = 0; i < pixels.Length; ++i)
            {
                if (pixels[i].A > 0.0f && pixels2[i].A > 0.0f)
                    return false;
            }

            

            return true;
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