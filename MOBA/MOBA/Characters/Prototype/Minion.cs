using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using System.IO;
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

        public Minion(int TeamID)
        {
            Team = TeamID;

            //Add preset animation to ani buffer
            //Maxhealth = Base amount * Towers dead
            maxHealth = 100;
            Health = 100;
            Position = new Vector2(150, 150);
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, WIDTH, HEIGHT);

            visionLayer = 0; // The minion can be seen by all light emissions
            defaultLayer = 0;

            //else set it to the original layer (ie invis or w/e)

            light = new LightEmitter();
        }

        public override void Update(GameTime gameTime)
        {
            setPosition((int)Position.X, (int)Position.Y);

            if (Health <= 0)
                light.Destroy();

            light.Update(Position);

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                Position.Y -= 2;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                Position.Y += 2;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                Position.X -= 2;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                Position.X += 2;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Main.Assets.getTexture(0).Texture, Bounds, Color.FromNonPremultiplied(255, 255, 255, (int)Alpha));
            spriteBatch.Draw(Main.Assets.getTexture(0).Texture, new Rectangle(Bounds.X, Bounds.Y - 10, Health, 5), Color.Red);
            //base.Draw(spriteBatch);
        }

        public bool isFriendly()
        {
            return (Team - Main.controller.player.Team) == 0;
        }

        public void changeVisibility(int x)
        {
            visionLayer = x;

            if (x == -1)
                visionLayer = defaultLayer;
        }
    }
}
