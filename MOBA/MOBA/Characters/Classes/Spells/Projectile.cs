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
using MOBA.Math;
using MOBA.World;

namespace MOBA.Characters.Classes.Spells
{
    public class Projectile
    {
        private Vector2 Start, End, Direction;
        private Ability spell;
        private Timer liveTime;
        private LightEmitter emitter;
        private float angle;
        private bool activeEmitter;

        public Projectile(Ability ability)
        {
            spell = ability;

            activeEmitter = spell.Emitter != null;

            Start = spell.pClass.Position;
            End = new Vector2(InputHandler.EventX, InputHandler.EventY);
            Direction = End - Start;

            angle = (float)System.Math.Atan2(End.Y, End.X);

            if (Direction != Vector2.Zero)
                Direction.Normalize();

            liveTime = new Timer(spell.SpellRange, false);

            if (activeEmitter)
            {
                emitter = new LightEmitter(Main.lightEngine, Start, spell.LightRadius, 0);
                Main.lightEngine.plugEmitter(emitter);
            }
        }

        public void Update()
        {
            liveTime.Run();

            if(activeEmitter)
                emitter.Update(Start);

            if (liveTime.Tick)
            {
                if(activeEmitter)
                    emitter.Destroy();

                spell.projectileList.Remove(this);
            }

            Start += Direction * 10f;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spell.image.Texture, new Rectangle((int)Start.X - spell.image.Texture.Width / 2, (int)Start.Y - spell.image.Texture.Height / 2, 40, 40), spell.image.sRect, Color.White, angle, new Vector2(0, 0), SpriteEffects.None, 0f);
        }
    }
}
