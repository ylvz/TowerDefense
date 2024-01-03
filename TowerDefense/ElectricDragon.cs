//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using SharpDX.Direct3D9;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Net.Mime.MediaTypeNames;

//namespace TowerDefense
//{
//    internal class ElectricDragon:Dragon
//    {
//        Vector2 pos;
//        private int timeSinceLastFrame = 0;
//        private int millisecondsPerFrame = 200;
//        private Point currentFrame = new Point(0, 0);
//        private Point sheetSize = new Point(5, 1);
//        private Point frameSize = new Point(121, 70);
//        int delay = 400;
//        int timeSinceLast = 0;
//        public ElectricDragon(Vector2 pos):base(pos)
//        {
//            this.pos = pos;
//        }


//        //public override void Update(GameTime gameTime)
//        //{
//        //    //timeSinceLast += gameTime.ElapsedGameTime.Milliseconds;
//        //    //if (timeSinceLast > delay)
//        //    //{
//        //    //PlayerAni(gameTime);
//        //    laser.Update(gameTime);

//        //}
//        public override void Draw(SpriteBatch spriteBatch)
//        {

//            spriteBatch.Draw(TextureHandler.elecDragonTex, pos, Color.White);
//            laser.Draw(spriteBatch);
//        }

//        public override void PlayerAni(GameTime gameTime)
//        {
//            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
//            if (timeSinceLastFrame > millisecondsPerFrame)
//            {
//                timeSinceLastFrame -= millisecondsPerFrame;
//                ++currentFrame.X;
//                if (currentFrame.X >= sheetSize.X)
//                {
//                    currentFrame.X = 0;
//                    ++currentFrame.Y;
//                    if (currentFrame.Y >= sheetSize.Y)
//                    {
//                        currentFrame.Y = 0;
//                    }
//                }
//            }
//        }
//    }
//}
