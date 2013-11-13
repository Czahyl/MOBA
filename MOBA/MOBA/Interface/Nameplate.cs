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
using System.Diagnostics;

namespace MOBA.Interface
{
    public class Nameplate
    {
        public BaseEntity entity
        { get; private set; }

        public Nameplate(Player e)
        {
            entity = e;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Vector2 Position = new Vector2((int)entity.Position.X - 32, (int)entity.Position.Y - entity.Height / 2 - 20);
            //int width = (entity.Health * 100 / entity.maxHealth) * 43;
            //spriteBatch.Draw(Main.Assets.getTexture(0).Texture, new Rectangle((int)Position.X + 16, (int)Position.Y + 1, width, 5), Color.Red);
            //spriteBatch.Draw(Main.Assets.getTexture(7).Texture, new Rectangle((int)Position.X, (int)Position.Y, 60, 15), Color.White);
        }
    }
}
