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
using MOBA.Math;

namespace MOBA.Characters.Classes.StatusEffects
{
    public class Burn : Debuff
    {
        public Burn(Player affectedPlayer, Player Owner) : base(affectedPlayer, Owner)
        {
            liveTime = new Timer(5, false);
            dotDmg = 5;
            dotTime = new Timer(1, false);
        }

        public Burn()
        {
            liveTime = new Timer(5, false);
            dotDmg = 5;
            dotTime = new Timer(1, false);
        }

        public override void Update(GameTime gameTime)
        {
            dotTime.Run();

            if (dotTime.Tick)
                Damage(dotDmg);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (dotTime.Tick)
                Main.particles.EmitParticles(affectedPlr.Position, Main.particles.presets["Fire"]);

            base.Draw(spriteBatch);
        }

    }
}
