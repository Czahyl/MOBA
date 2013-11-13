using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MOBA.Characters.Prototype;
using MOBA.Input;
using MOBA.World;
using MOBA.Math;
using MOBA.Assets;

namespace MOBA.Characters.Classes.Spells
{
    public class Fireball : Ability
    {
        public Fireball(Player player) : base(player)
        {
            pClass = player;

            Emitter = new LightEmitter();
            LightRadius = 35f;

            image = Main.Assets.getTexture(3);

            Damage = pClass.SpellPower + (10 * pClass.Level);

            Speed = 10f;

            SpellRange = 1f;
            cooldown = new Timer(60, false);
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < projectileList.Count; i++)
            {
                projectileList[i].Update();
            }

            if (Selecting)
            {
                if (InputHandler.EventButton == MouseButton.Left)
                    Cast(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Cast(GameTime gameTime)
        {
            if (!onCooldown)
            {
                projectileList.Add(new Projectile(this));
                base.Cast(gameTime);
            }
        }

        public void drawSelection(SpriteBatch spriteBatch)
        {
            Image image = Main.Assets.getTexture(4);
            spriteBatch.Draw(image.Texture, new Rectangle((int)pClass.Position.X, (int)pClass.Position.Y, 140, 40), null, Color.RoyalBlue, (float)System.Math.Atan2(InputHandler.EventY - pClass.Position.Y, InputHandler.EventX - pClass.Position.X), new Vector2(0, image.Texture.Height / 2), SpriteEffects.None, 0f);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < projectileList.Count; i++)
                projectileList[i].Draw(spriteBatch);

            if(Selecting)
                drawSelection(spriteBatch);

            base.Draw(spriteBatch);
        }
    }
}
