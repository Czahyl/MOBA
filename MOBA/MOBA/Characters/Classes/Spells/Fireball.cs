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
using MOBA.Input;
using MOBA.World;
using MOBA.Math;

namespace MOBA.Characters.Classes.Spells
{
    public class Fireball : Ability
    {
        public Fireball(Player player) : base(player)
        {
            pClass = player;

            image = Main.assets.getTexture(3);

            Damage = pClass.SpellPower + (10 * pClass.Level);

            drawTime = new Timer(0.5f);

            Position = pClass.Position;

            Direction = new Vector2((int)InputHandler.EventX, (int)InputHandler.EventY) - Position;

            Rect = new Rectangle((int)Position.X, (int)Position.Y, 100, 100);
        }

        public override void Cast(GameTime gameTime)
        {
            drawTime.Run();

            hitRect = new Rectangle((int)Position.X, (int)Position.Y, 50, 50);

            if(Alive)
                Position += Direction * 5f;

            if (drawTime.Tick)
                Alive = false;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image.Texture, new Rectangle((int)Position.X, (int)Position.Y, Rect.Width, Rect.Height), image.sRect, Color.White);
            base.Draw(spriteBatch);
        }
    }
}
