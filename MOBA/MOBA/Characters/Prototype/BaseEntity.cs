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
        public Rectangle Bounds { get; set; }
        public Vector2 Center { get; set; }
        public Texture2D Texture {get; protected set;}
        public LightEmitter light { get; set; }
        protected int maxHealth;
        public int Health { get; protected set; }
        public Rectangle hpRect;
        public int invisibilityLayer { get; protected set; }

        public Animation ani;

        protected bool locked = false;

        public BaseEntity()
        {
            Bounds = Rectangle.Empty;
            Center = Vector2.Zero;

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

        public virtual void Update()
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
            spriteBatch.Draw(Texture, Bounds, Color.White);
        }

        public virtual void Move(int x, int y)
        {
            if (locked) return;
            Bounds.Offset(x, y);
        }

        public virtual void setPosition(int x, int y)
        {
            if (locked) return;
            Bounds = new Rectangle(x, y, Bounds.Width, Bounds.Height);
        }

        public virtual void Damage(int amount)
        {
            Health -= amount;
        }
    }
}
