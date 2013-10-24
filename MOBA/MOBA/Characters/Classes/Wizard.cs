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
        private bool cast = false;

        Ability currentAbility;

        public Wizard() : base()
        {
            Attack = 5;
            SpellPower = 15;

            currentAbility = new Ability(this);
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.keyPressed(Keys.D1))
            {
                currentAbility = new Fireball(this);
                currentAbility.Alive = true;
            }

            if (currentAbility.Alive)
                currentAbility.Cast(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(currentAbility.Alive)
                currentAbility.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
    }
}
