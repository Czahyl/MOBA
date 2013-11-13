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

namespace MOBA.Characters.Classes
{
    public class Wizard : Player
    {
        Animation idle, attack;
        Ability fireball;

        public Wizard(string Username, int TeamID) : base(Username, TeamID)
        {
            Animations.Add("Idle", new Animation(5));
            Animations.Add("Attack", new Animation(5));

            idle = Animations["Idle"];
            attack = Animations["Attack"];

            currentAni = new Animation(5);

            idle.buffer.Add(Main.Assets.getTexture(5));
            attack.buffer.Add(Main.Assets.getTexture(6));

            currentAni = idle;

            stance = Main.Assets.getTexture(5).Texture;
            Attack = 20;
            SpellPower = 15;

            fireball = new Fireball(this);
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.keyPressed(Keys.D1) && !fireball.onCooldown)
            {
                fireball.Select();
            }

            fireball.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            fireball.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
    }
}
