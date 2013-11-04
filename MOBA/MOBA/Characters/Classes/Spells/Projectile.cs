﻿using System;
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
    public class Projectile
    {
        public Rectangle Rect { get; private set; }
        public int dmg = 10;
        Vector2 Start, End, Direction;
        private float speed = 10f;

        private Player plr;

        LightEmitter emitter;

        private Image image;
        private Timer timer;

        public Projectile(Vector2 startPos, Player player)
        {
            Start = startPos;
            plr = player;
            image = Main.Assets.getTexture(3);
            End = new Vector2((float)InputHandler.EventX, (float)InputHandler.EventY);

            timer = new Timer(Player.AttRange, false); // Create the live time of the projectile

            Direction = End - Start;

            if (Direction != Vector2.Zero)
                Direction.Normalize();
        }

        public Projectile()
        {

        }

        public bool Hit(Rectangle rect)
        {
            if (!Rect.Intersects(plr.Bounds))
            {
                return Rect.Intersects(rect);
            }

            return Rect.Intersects(rect);
        }

        public void Destroy()
        {
            plr.autoAttack.Remove(this);
        }

        public void Shoot(GameTime gameTime)
        {
            timer.Run();

            if (timer.Tick)
                Destroy();

            Start.X += (Direction.X * speed);
            Start.Y += (Direction.Y * speed);

            foreach(PlayerController player in Main.Players)
            {
                if (Hit(player.getPlayer().Rect()))
                {
                    player.getPlayer().Damage(plr.Attack);
                    Destroy();
                }
            }

            foreach(MinionController minion in Main.Minions)
            {
                if (Hit(minion.getEntity().Rect()))
                {
                    minion.getEntity().Damage(plr.Attack);
                    Destroy();
                }
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
