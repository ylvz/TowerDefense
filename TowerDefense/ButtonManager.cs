using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.IO;

namespace TowerDefense
{
    internal class ButtonManager
    {

        Vector2 startPos, quitPos, storyPos, againPos, quitPos2, againPos2, quitPos3;
        Rectangle startRect, quitRect, storyRect, againRect, quitRect2, againRect2, quitRect3;
        StoryScreen storyScreen;
        bool open;

        public ButtonManager()
        {
            startPos = new Vector2(247,590);
            quitPos = new Vector2(240, 680);
            storyPos = new Vector2(247, 635);
            startRect = new Rectangle((int)startPos.X, (int)startPos.Y, TextureHandler.startButton.Width, TextureHandler.startButton.Height);
            quitRect = new Rectangle((int)quitPos.X, (int)quitPos.Y, TextureHandler.quitButton.Width, TextureHandler.quitButton.Height);
            storyRect = new Rectangle((int)storyPos.X, (int)storyPos.Y, TextureHandler.storyButton.Width, TextureHandler.storyButton.Height);

            againPos = new Vector2(160, 720);
            quitPos2 = new Vector2(260, 720);
            againRect = new Rectangle((int)againPos.X, (int)againPos.Y, TextureHandler.againTex.Width, TextureHandler.againTex.Height);
            quitRect2 = new Rectangle((int)quitPos2.X, (int)quitPos2.Y, TextureHandler.quitTex.Width, TextureHandler.quitTex.Height);

            againPos2 = new Vector2(480, 360);
            quitPos3 = new Vector2(600, 360);
            againRect2 = new Rectangle((int)againPos2.X, (int)againPos2.Y, TextureHandler.againTex.Width, TextureHandler.againTex.Height);
            quitRect3 = new Rectangle((int)quitPos3.X, (int)quitPos3.Y, TextureHandler.quitTex.Width, TextureHandler.quitTex.Height);

            open = false;

        }

        public void Update(GameTime gameTime,Game1 game1,string gameScreen)
        {
            Point mousePosition = new Point(Mouse.GetState().X, Mouse.GetState().Y);
            if (gameScreen == "mainmenu")
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {


                    if (startRect.Contains(mousePosition))
                    {
                        game1.SwitchToLevel1();
                    }
                    if (quitRect.Contains(mousePosition))
                    {
                        game1.Exit();
                    }
                    if (storyRect.Contains(mousePosition) && !open)
                    {
                        storyScreen = new StoryScreen();
                        open = true;
                        storyScreen.Show();
                    }

                }
            }

            if (gameScreen == "loosescreen")
            {
                if(Mouse.GetState().LeftButton == ButtonState.Pressed)
                {

                    if (againRect.Contains(mousePosition))
                    {
                        game1.SwitchToLevel1();
                    }

                    if (quitRect2.Contains(mousePosition))
                    {
                        game1.Exit();
                    }
                }
            }

            if (gameScreen == "winscreen")
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    if (againRect2.Contains(mousePosition))
                    {
                        game1.SwitchToLevel1();
                    }

                    if (quitRect3.Contains(mousePosition))
                    {
                        game1.Exit();
                    }
                }
            }

            
        }

        public void Draw(SpriteBatch spriteBatch, string gameScreen)
        {
            if (gameScreen == "mainmenu")
            {
                spriteBatch.Draw(TextureHandler.startButton, startPos, Color.White);
                spriteBatch.Draw(TextureHandler.quitButton, quitPos, Color.White);
                spriteBatch.Draw(TextureHandler.storyButton, storyPos, Color.White);
            }

            if (gameScreen == "loosescreen")
            {
                spriteBatch.Draw(TextureHandler.againTex, againPos, Color.White);
                spriteBatch.Draw(TextureHandler.quitTex, quitPos2, Color.White);
            }

            if (gameScreen == "winscreen")
            {
                spriteBatch.Draw(TextureHandler.againTex, againPos2, Color.White);
                spriteBatch.Draw(TextureHandler.quitTex, quitPos3, Color.White);
            }

        }
    }
}
