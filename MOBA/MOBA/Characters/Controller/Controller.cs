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
using MOBA.Characters.Prototype;

namespace MOBA.Characters.Controller
{
    public class Controller
    {
        public Player player;
        protected Main m;

        public Controller(Main main)
        {
            m = main;
        }

        public virtual void plugEntity(Player p)
        {
            player = p;

        }

        public virtual void Update(GameTime gameTime)
        {
            player.Update(gameTime);
        }

        public virtual void Draw()
        {
            player.Draw(m.spriteBatch);
        }
    }
}
