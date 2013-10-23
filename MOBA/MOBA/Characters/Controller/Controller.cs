using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MOBA.Characters.Prototype;

namespace MOBA.Characters.Controller
{
    public class Controller
    {
        protected Player player;
        protected Main main;

        public Controller(Main main)
        {
            this.main = main;
        }

        public virtual void plugEntity(Player entity)
        {
            player = entity;
        }

        public virtual void Update(GameTime time)
        {

        }
    }
}
