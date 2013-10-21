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

            minion.light = new LightEmitter(m.lightEngine, (int)minion.Center.X, (int)minion.Center.Y, 75, false, 0);

            m.lightEngine.plugEmitter(minion.light);
        }

        public void Update()
        {
            minion.Update();
        }

        public void Draw()
        {
            minion.Draw(m.spriteBatch);
        }
    }
}
