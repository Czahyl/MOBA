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
        public Point Center { get; protected set; }
        public Vector2 Position;
        public Image Image { get; protected set; }
        public float Alpha { get; protected set; }
        public LightEmitter light { get; set; }
        public Animation ani { get; set; }
        public int Health { get; protected set; }
        public int moveSpeed { get; protected set; }
        public int visionLayer { get; protected set; }
        public int defaultLayer { get; protected set; }

        protected int maxHealth;

        protected bool locked = false;

        public BaseEntity()
        {
            Bounds = Rectangle.Empty;
            Center = Point.Zero;
            Position = Vector2.Zero;

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
            }

            Bounds = new Rectangle((int)Position.X, (int)Position.Y, 32, 64);
            Center = new Point(Bounds.Center.X, Bounds.Center.Y);
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

        public void setAlpha(float x)
        {
            Alpha = x;
        }

        public virtual void Damage(int amount)
        {
            Health -= amount;
        }
    }
}
