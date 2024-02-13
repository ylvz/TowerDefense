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

        Vector2 startPos, quitPos, storyPos;
        Rectangle startRect, quitRect, storyRect;
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
            open = false;

        }

        public void Update(GameTime gameTime,Game1 game1)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {

                Point mousePosition = new Point(Mouse.GetState().X, Mouse.GetState().Y);
                
                if (startRect.Contains(mousePosition))
                {
                    game1.SwitchToLevel1();
                }
                if (quitRect.Contains(mousePosition))
                {
                    game1.Exit();
                }
                if (storyRect.Contains(mousePosition)&&!open)
                {
                    storyScreen = new StoryScreen();
                    open = true;
                    storyScreen.Show();
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureHandler.startButton, startPos, Color.White);
            spriteBatch.Draw(TextureHandler.quitButton, quitPos, Color.White);
            spriteBatch.Draw(TextureHandler.storyButton, storyPos, Color.White);
        }
    }
}
