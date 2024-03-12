using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    abstract class GameObject
    {
        protected Vector2 pos;
        protected Texture2D tex;
        protected Rectangle hitBox;

        public Rectangle HitBox
        {
            get { return hitBox; }
        }


        public GameObject(Rectangle rect)
        {
            this.hitBox = rect;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
