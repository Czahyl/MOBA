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

        public int Team
        { get; private set; }

        public bool Friendly;

        public Minion(int TeamID)
        {
            Team = TeamID;

            Friendly = (Team - Main.controller.Team) == 0;

            //Add preset animation to ani buffer
            //Maxhealth = Base amount * Towers dead
            maxHealth = 100;
            Health = 100;
            Position = new Vector2(150, 150);
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, WIDTH, HEIGHT);

            if(Friendly)
                invisibilityLayer = 0; // The minion can be seen by all light emissions
            //else set it to the original layer (ie invis or w/e)

            light = new LightEmitter();
        }

        public override void Update(GameTime gameTime)
        {
            setPosition((int)Position.X, (int)Position.Y);

            if (Health <= 0)
                light.Destroy();

                light.Update(Position);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Main.Assets.getTexture(0).Texture, Rect(), Color.White);
            spriteBatch.Draw(Main.Assets.getTexture(0).Texture, new Rectangle((int)Position.X, (int)Position.Y - 10, Health / 2, 5), Color.Red);
            //base.Draw(spriteBatch);
        }
    }
}
