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
using MOBA.Characters.Controller;
using MOBA.Characters.Prototype;
using MOBA.Assets;

namespace MOBA.Characters.Classes.Spells
{
    public class Projectile
    {
        private Vector2 Start, End, Direction;
        private Ability spell;
        private Timer liveTime;
        private LightEmitter emitter;
        private Image image;
        private Rectangle rect;
        private float angle;
        private bool activeEmitter;
        private int damage;

        public Projectile(Ability ability)
        {
            spell = ability;

            activeEmitter = spell.Emitter != null;

            Start = spell.pClass.Position;
            End = new Vector2(InputHandler.EventX, InputHandler.EventY);
            Direction = End - Start;

            damage = ability.Damage;

            if (Direction != Vector2.Zero)
                Direction.Normalize();

            liveTime = new Timer(spell.SpellRange, false);

            if (activeEmitter)
            {
                emitter = new LightEmitter(Main.lightEngine, Start, spell.LightRadius, 1);
                Main.lightEngine.plugEmitter(emitter);
            }

            angle = (float)System.Math.Atan2(Direction.Y, Direction.X);
        }

        public void Update()
        {
            liveTime.Run();

            rect = new Rectangle((int)Start.X, (int)Start.Y, 60, 40); // change to image later

            if(activeEmitter)
                emitter.Update(Start);

            if (liveTime.Tick)
            {
                if(activeEmitter)
                    emitter.Destroy();

                spell.projectileList.Remove(this);
            }

            Start += Direction * 10f;

            foreach (MultiplayerController entity in Main.Players)
            {
                Player current = entity.player;

                if (rect.Intersects(current.Bounds))
                {
                    current.Damage(damage);
                    spell.projectileList.Remove(this);
                    emitter.Destroy();
                } 
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //TODO fill out rect & image
            spriteBatch.Draw(spell.image.Texture, new Rectangle((int)Start.X, (int)Start.Y, 60, 40), spell.image.sRect, Color.White, angle, new Vector2(0, 7), SpriteEffects.None, 0f);
        }
    }
}
