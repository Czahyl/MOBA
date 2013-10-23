using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MOBA.Assets;
using MOBA.World;

namespace MOBA.Characters.Prototype
{
    public class BaseEntity
    {
        public Rectangle Bounds { get; protected set; }
        public Vector2 Position;
        public Image Image {get; protected set;}
        public LightEmitter light { get; set; }
        protected int maxHealth;
        public int Health { get; protected set; }
        public int moveSpeed { get; protected set; }
        public int invisibilityLayer { get; protected set; }

        public Animation ani;

        protected bool locked = false;

        public BaseEntity()
        {
            Bounds = Rectangle.Empty;
            Position = Vector2.Zero;

            //Position.X = Position.X + Image.Texture.Width / 2;
            //Position.Y = Position.Y - Image.Texture.Height / 2;

            light = new LightEmitter();
        }

        public void Lock()
        {
            locked = true;
        }

        public void unLock()
        {
            locked = false;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Health > maxHealth)
            {
                Health = maxHealth;
            }

            if (Health < 0)
            {
                Health = 0;
                light.Destroy();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Image.Texture, Bounds, Color.White);
        }

        public virtual void Move(int x, int y)
        {
            if (locked) return;
            Bounds.Offset(x, y);
        }

        public virtual void setPosition(int x, int y)
        {
            if (locked) return;
            Position = new Vector2(x, y);
        }

        public Rectangle Rect()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, 32, 64);
        }

        public virtual void Damage(int amount)
        {
            Health -= amount;
        }
    }
}
