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
using MOBA.Input;
using MOBA.Assets;
using MOBA.Math;

namespace MOBA.Characters.Classes.Spells
{
    public class Projectile
    {
        public Rectangle Rect { get; private set; }
        public int dmg = 10;
        Vector2 Start, End, Direction;
        private float speed = 5f;

        private Image image;
        private Timer timer;

        public Projectile(Vector2 startPos)
        {
            Start = startPos;
            image = Main.assets.getTexture(3);
            End = new Vector2((float)InputHandler.EventX, (float)InputHandler.EventY);

            timer = new Timer(5);

            Direction = End - Start;

            if (Direction != Vector2.Zero)
                Direction.Normalize();
        }

        public Projectile()
        {

        }

        public void Shoot(GameTime gameTime)
        {
            timer.Run();

            if (!timer.Tick)
            {
                Start.X += (Direction.X * speed);
                Start.Y += (Direction.Y * speed);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rect = new Rectangle((int)Start.X, (int)Start.Y, 10, 10);
            float angle = (float)System.Math.Atan2(Direction.Y, Direction.X);
            spriteBatch.Draw(image.Texture, new Rectangle((int)Start.X, (int)Start.Y, image.Texture.Width, image.Texture.Height), image.sRect, Color.White, angle, new Vector2(0, 0), SpriteEffects.None, 0f);
        }
    }
}
