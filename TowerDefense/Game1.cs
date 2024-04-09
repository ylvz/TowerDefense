using CatmullRom;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ParticleEngine2D;
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
            Window.Title = "Forest Defense";

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
                    buttonManager.Update(gameTime, this,"mainmenu");
                    break;

                case GameState.Level1:
                    {
                        MouseState currentMouseState = KeyMouseReader.mouseState;
                        animalManager.Update(gameTime, enemyManager, previousMouseState); // Pass previousMouseState here
                        previousMouseState = currentMouseState; // Update previousMouseState for the next frame
                        DrawOnRenderTarget(levelManager.levels[0]);
                        enemyManager.Update(gameTime);
                        forest.Update(this);
                        enemyManager.CollisionWithForest(forest);
                        for (int i = 0; i < animalManager.GetMoose().Count; i++)
                        {
                            enemyManager.CollisionDetection(animalManager.GetAllLasers(), gameTime);
                        }
                        for (int i = 0; i < animalManager.GetWolf().Count; i++)
                        {
                            enemyManager.CollisionDetection(animalManager.GetAllLasers(), gameTime);
                        }
                        for (int i = 0; i < animalManager.GetHog().Count; i++)
                        {
                            enemyManager.CollisionDetection(animalManager.GetAllLasers(), gameTime);
                        }

                        base.Update(gameTime);
                    }
                    break;

                case GameState.WinScreen:

                    buttonManager.Update(gameTime, this, "winscreen");
                    break;

                case GameState.LooseScreen:
                    buttonManager.Update(gameTime, this, "loosescreen");
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
                    buttonManager.Draw(_spriteBatch,"mainmenu");
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

                case GameState.WinScreen:
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(TextureHandler.winBgTex, GraphicsDevice.Viewport.Bounds, Color.White);
                    buttonManager.Draw(_spriteBatch, "winscreen");
                    _spriteBatch.End();
                    break;

                case GameState.LooseScreen:
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(TextureHandler.looseBgTex,GraphicsDevice.Viewport.Bounds, Color.White);
                    buttonManager.Draw(_spriteBatch, "loosescreen");
                    _spriteBatch.End();
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

        public void SwitchToStory()
        {
            currentGameState = GameState.Level1;
        }

        public void SwitchToWin()
        {
            currentGameState = GameState.WinScreen;
        }

        public void SwitchToLoose()
        {
            currentGameState = GameState.LooseScreen;
        }

        public void SwitchToLevel1()
        {
            currentGameState = GameState.Level1;
            enemyManager.enemies.Clear();
            enemyManager.strongEnemies.Clear();
            forest.maxLife = 15;
            LoadContent();
        }

    }
}