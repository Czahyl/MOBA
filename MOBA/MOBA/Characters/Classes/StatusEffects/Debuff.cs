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
    public class Debuff
    {
        public Player affectedPlr;
        public Player owner;
        protected int dotDmg;

        protected Timer liveTime;
        protected Timer dotTime;


        public Debuff(Player affectedPlayer, Player Owner)
        {
            affectedPlr = affectedPlayer;
            owner = Owner;
        }

        public Debuff()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            liveTime.Run();

            if (liveTime.Tick)
                affectedPlr.debuffList.Remove(this);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void Slow(float amount)
        {
            affectedPlr.MoveSpeed *= amount;
        }

        public virtual void Damage(int amount)
        {
            affectedPlr.Damage(amount);
        }
    }
}
