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
using MOBA.Assets;
using MOBA.Characters.Controller;
using MOBA.Characters.Classes;
using MOBA.Characters.Classes.StatusEffects;
using MOBA.Characters.Prototype;
using MOBA.World;
using MOBA.Input;
using MOBA.Math;

namespace MOBA.Characters.Classes.Spells
{
    public class Ability
    {
        public Player pClass;

        public static Ability None;

        public List<Projectile> projectileList
        { get; protected set; }

        public int Cost
        { get; protected set; }

        public Image image
        { get; protected set; }

        public int Damage
        { get; protected set; }

        public Debuff debuff
        { get; protected set; }

        public Buff buff
        { get; protected set; }

        public float Speed
        { get; protected set; }

        public Rectangle Rect
        { get; protected set; }

        public Rectangle hitRect
        { get; protected set; }

        public float SpellRange
        { get; protected set; }

        public LightEmitter Emitter
        { get; protected set; }

        public float LightRadius
        { get; protected set; }

        public bool Selecting = false;

        public bool Alive = false;

        public Vector2 Position;

        protected Timer cooldown;

        public bool onCooldown
        { get; protected set; }

        public Ability(Player player)
        {
            pClass = player;

            projectileList = new List<Projectile>();
        }

        public virtual void Update(GameTime gameTime)
        {
            if (onCooldown)
                cooldown.Run();

            if (cooldown.Tick)
                onCooldown = false;
        }

        public virtual void Select()
        {
            Selecting = !Selecting;
            pClass.canAttack = !pClass.canAttack;
        }

        public void failedCast()
        {
            onCooldown = false;
        }

        public void removeProjectile(Projectile p)
        {
            projectileList.Remove(p);
        }

        public virtual void Cast(GameTime gameTime)
        {
            if (!onCooldown)
            {
                cooldown = new Timer(60, false);
                onCooldown = true;
                Selecting = false;
                pClass.canAttack = true;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(image.Texture, new Rectangle((int)Position.X, (int)Position.Y, Rect.Width, Rect.Height), image.sRect, Color.White);
        }
    }
}
