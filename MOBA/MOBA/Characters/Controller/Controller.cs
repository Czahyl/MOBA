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
        protected Main game;

        public Controller(Main main)
        {
            game = main;
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
