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
using MOBA.Characters.Prototype;
using MOBA.Characters.Controller;
using MOBA.World;

namespace MOBA.Characters.Classes.Spells
{
    public class Autoattack
    {
        public Rectangle Rect { get; private set; }
        Vector2 Start, End, Direction;
        private float speed = 500f;

        private Player plr;

        private Image image;
        private Timer timer;

        private float angle;

        public Autoattack(Vector2 startPos, Player player)
        {
            Start = startPos;
            plr = player;
            image = Main.Assets.getTexture(3);
            End = new Vector2((float)InputHandler.EventX, (float)InputHandler.EventY);

            timer = new Timer(Player.AttRange, false); // Create the live time of the projectile

            Direction = End - Start;

            angle = (float)System.Math.Atan2(Direction.Y, Direction.X);

            if (Direction != Vector2.Zero)
                Direction.Normalize();
        }

        public Autoattack()
        {

        } 

        public void Destroy()
        {
            plr.removeProjectile(this);
        }

        public void Shoot(GameTime gameTime)
        {
            timer.Run();

            if (timer.Tick || End == plr.Position)
                Destroy();

            Start += Direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(Main.hitPlayer(Rect) != null)
            {
                Player player = Main.hitPlayer(Rect);
                if (!player.isFriendly())
                {
                    player.Damage(plr.Attack);
                    Destroy();
                }
            }

            if(Main.hitMinion(Rect) != null)
            {
                Minion entity = Main.hitMinion(Rect);
                if (!entity.isFriendly())
                {
                    entity.Damage(plr.Attack);
                    Destroy();
                }
                else if (entity.isFriendly())
                    Destroy();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rect = new Rectangle((int)Start.X, (int)Start.Y, 10, 10);
            spriteBatch.Draw(image.Texture, new Rectangle((int)Start.X, (int)Start.Y, image.Texture.Width, image.Texture.Height), image.sRect, Color.White, angle, new Vector2(0, 0), SpriteEffects.None, 0f);
        }
    }
}
