using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MOBA.World;

namespace MOBA.Characters.Prototype
{
    public class Minion : BaseEntity
    {
        public const int WIDTH = 32;
        public const int HEIGHT = 32;
        public const int WORTH = 10;

        public Minion()
        {
            //Add preset animation to ani buffer
            //Maxhealth = Base amount * Towers dead
            maxHealth = 100;
            Health = 100;
            invisibilityLayer = 0; // The minion can be seen by all light emissions
            Position = new Vector2(150, 150);
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, WIDTH, HEIGHT);
            light = new LightEmitter();
        }

        public override void Update(GameTime gameTime)
        {
            setPosition((int)Position.X, (int)Position.Y);

            if (Health <= 0)
                light.Destroy();
            //base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Main.assets.getTexture(0).Texture, Rect(), Color.White);
            spriteBatch.Draw(Main.assets.getTexture(0).Texture, new Rectangle((int)Position.X, (int)Position.Y - 10, Health / 2, 5), Color.Red);
            base.Draw(spriteBatch);
        }
    }
}
