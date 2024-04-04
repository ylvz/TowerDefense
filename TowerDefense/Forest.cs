using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TowerDefense
{
    internal class Forest
    {

        public int maxLife = 20;
        public Vector2 rectPos;
        public Rectangle hitBox;
        private Vector2 pricePos1, pricePos2, pricePos3;
        private Vector2 moneyPos;
        public static int money;


        public Forest()
        {
            hitBox = new Rectangle(993, 258, 156, 222);
            pricePos1 = new Vector2(400, 650);
            pricePos2 = new Vector2(550, 650);
            pricePos3 = new Vector2(700, 650);
            moneyPos = new Vector2(960, 50);
            money = 1000;

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // Draw the forest texture
            spriteBatch.Draw(TextureHandler.forestTex, hitBox, Color.White);

            // Calculate the position of the health bar above the forest
            int healthBarX = hitBox.X - TextureHandler.healthTex.Width - 5;
            int healthBarY = hitBox.Y - TextureHandler.healthTex.Height - 28;
            int segmentWidth = 40;

            // Draw health bar segments
            for (int i = 0; i < maxLife; i++)
            {
                Rectangle segmentRect = new Rectangle(healthBarX + i * TextureHandler.healthTex.Width, healthBarY, segmentWidth, TextureHandler.healthTex.Height);
                spriteBatch.Draw(TextureHandler.healthTex, segmentRect, Color.Red);
            }
            spriteBatch.Draw(TextureHandler.priceTex1, pricePos1, Color.White);
            spriteBatch.Draw(TextureHandler.priceTex2, pricePos2, Color.White);
            spriteBatch.Draw(TextureHandler.priceTex3, pricePos3, Color.White);
            spriteBatch.DrawString(TextureHandler.spriteFont, "Credit: " + money+ " $", moneyPos, Color.White);
            spriteBatch.End();
        }



    }

}
