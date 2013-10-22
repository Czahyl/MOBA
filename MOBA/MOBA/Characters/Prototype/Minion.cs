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
            Health = 100;
            invisibilityLayer = 0; // The minion can be seen by all light emissions
            Center = new Vector2(150, 150);
            Bounds = new Rectangle((int)Center.X, (int)Center.Y, WIDTH, HEIGHT);
            light = new LightEmitter();
        }

        public override void Update(GameTime gameTime)
        {
            setPosition((int)Center.X + Camera.X, (int)Center.Y + Camera.Y);
            hpRect = new Rectangle(((int)Center.X + Camera.X) + 10, (int)Center.Y + Camera.Y, Health / 10, 5);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Main.assets.getTexture(0).Texture, hpRect, Color.Red);
            base.Draw(spriteBatch);
        }
    }
}
