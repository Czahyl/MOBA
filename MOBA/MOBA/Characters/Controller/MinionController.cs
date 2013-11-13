using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MOBA.Characters.Prototype;
using MOBA.World;

namespace MOBA.Characters.Controller
{
    public class MinionController
    {
        private Main m;
        public Minion entity;

        public MinionController(Main main)
        {
            m = main;
        }

        public void plugEntity(Minion m)
        {
            entity = m;

            entity.light = new LightEmitter(Main.lightEngine, entity.Position, 75, 0);

            if(entity.isFriendly())
                Main.lightEngine.plugEmitter(entity.light);
        }

        public void Update(GameTime gameTime)
        {
            entity.Update(gameTime);

            if (entity.Health <= 0)
            {
                Main.Minions.Remove(this);
            }
        }

        public void Draw()
        {
            entity.Draw(m.spriteBatch);
        }
    }
}
