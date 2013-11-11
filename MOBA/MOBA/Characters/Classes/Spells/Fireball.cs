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

namespace MOBA.Characters.Classes.Spells
{
    public class Fireball : Ability
    {
        public Fireball(Player player) : base(player)
        {
            pClass = player;

            Emitter = new LightEmitter();
            LightRadius = 25f;

            image = Main.Assets.getTexture(3);

            Damage = pClass.SpellPower + (10 * pClass.Level);

            Speed = 10f;

            SpellRange = 1f;
            cooldown = new Timer(60, false);
        }

        public override void Update()
        {
            for (int i = 0; i < projectileList.Count; i++)
            {
                projectileList[i].Update();
            }

            base.Update();
        }

        public void Select()
        {

        }

        public override void Cast(GameTime gameTime)
        {
            if (!onCooldown)
            {
                projectileList.Add(new Projectile(this));
                cooldown = new Timer(60, false);
                onCooldown = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < projectileList.Count; i++)
                projectileList[i].Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
    }
}
