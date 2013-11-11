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
        Ability fireball;

        public Wizard(string Username, int TeamID) : base(Username, TeamID)
        {
            Attack = 20;
            SpellPower = 15;

            fireball = new Fireball(this);
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.keyPressed(Keys.D1))
            {
                fireball.Cast(gameTime);
            }

            fireball.Update();

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            fireball.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
    }
}
