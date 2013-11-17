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
 using MOBA.Characters.Classes.StatusEffects;
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
        private Debuff db;
        private float angle;
        private bool activeEmitter;
        private int damage;
        private float speed = 500f;

        public Projectile(Ability ability, Debuff debuff)
        {
            spell = ability;

            db = debuff;

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

            if (End == spell.pClass.Position)
            {
                spell.failedCast();
                spell.projectileList.Remove(this);
            }

            spell.pClass.Drain(spell.Cost);
        }

        public void Update(GameTime gameTime)
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

            Start += Direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < Main.Players.Count; i++)
            {
                Player current = Main.Players[i].player;

                if (rect.Intersects(current.Bounds) && current.Friendly)
                {
                    current.Damage(damage);

                    db.affectedPlr = current; // debuff presets
                    db.owner = spell.pClass;
                    current.debuffList.Add(db);

                    emitter.Destroy();
                    spell.removeProjectile(this);
                } 
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //TODO fill out rect & imag
            spriteBatch.Draw(spell.image.Texture, rect, spell.image.sRect, Color.White, angle, new Vector2(0, 7), SpriteEffects.None, 0f);
        }
    }
}
