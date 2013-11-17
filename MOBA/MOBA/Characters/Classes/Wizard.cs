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
using MOBA.Characters.Prototype;
using MOBA.Characters.Classes.Spells;
using MOBA.Input;
using MOBA.Assets;
using MOBA.Math;
using MOBA.Interface;

namespace MOBA.Characters.Classes
{
    public class Wizard : Player
    {
        Animation idle, attack;

        public Wizard(string Username, int TeamID) : base(Username, TeamID)
        {
            BaseHealth = 75;
            Health = BaseHealth;
            maxHealth = (BaseHealth * Level) + HealthStat;

            BaseMana = 125;
            Mana = BaseMana;
            maxMana = (BaseMana * Level) + ManaStat;

            nameplate = new Nameplate(this);
            Animations.Add("Attack", new Animation(5));
            Animations.Add("Idle", new Animation(5));

            idle = Animations["Idle"];
            attack = Animations["Attack"];

            currentAni = new Animation(5);

            idle.buffer.Add(Main.Assets.getTexture(5));
            attack.buffer.Add(Main.Assets.getTexture(6));

            currentAni = idle;

            Attack = 10 + AttackPower;
            SpellPower = 15;

            ability.Add(new Fireball(this));
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < ability.Count; i++)
                ability[i].Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < ability.Count; i++)
                ability[i].Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
    }
}
