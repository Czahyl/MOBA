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
        protected Minion minion;

        public MinionController(Main main)
        {
            m = main;
        }

        public Minion getEntity()
        {
            return minion;
        }

        public void plugEntity(Minion entity)
        {
            minion = entity;

            minion.light = new LightEmitter(Main.lightEngine, entity.Position, 75, 0);

            Main.lightEngine.plugEmitter(minion.light);
        }

        public void Update(GameTime gameTime)
        {
            minion.Update(gameTime);

            if (minion.Health <= 0)
                Main.Minions.Remove(this);
        }

        public void Draw()
        {
            minion.Draw(m.spriteBatch);
        }
    }
}
