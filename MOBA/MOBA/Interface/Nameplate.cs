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
        public Player entity
        { get; private set; }

        Texture2D rect, nameplate;
        SpriteFont nameplateFont;

        public Nameplate(Player e)
        {
            entity = e;

            rect = Main.Assets.getTexture(0).Texture;
            nameplate = Main.Assets.getTexture(7).Texture;
            nameplateFont = Main.Assets.getFont(0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 Position = new Vector2((int)entity.Position.X - 32, (int)entity.Position.Y - entity.Height / 2 - 20);
            double percentHP = (double)entity.Health / (double)entity.maxHealth;
            double hpWidth = System.Math.Round(43 * percentHP);
            double percentMana = (double)entity.Mana / (double)entity.maxMana;
            double manaWidth = System.Math.Round(43 * percentMana);
            spriteBatch.Draw(rect, new Rectangle((int)Position.X + 16, (int)Position.Y + 1, 43, 5), new Color(60, 60, 60));
            spriteBatch.Draw(rect, new Rectangle((int)Position.X + 16, (int)Position.Y + 1, (int)hpWidth, 5), Color.Red);
            spriteBatch.Draw(rect, new Rectangle((int)Position.X + 16, (int)Position.Y + 9, 43, 5), new Color(60, 60, 60));
            spriteBatch.Draw(rect, new Rectangle((int)Position.X + 16, (int)Position.Y + 9, (int)manaWidth, 5), Color.Blue);
            Debug.WriteLine(manaWidth);
            spriteBatch.Draw(nameplate, new Rectangle((int)Position.X, (int)Position.Y, 60, 15), Color.White);
            spriteBatch.DrawString(nameplateFont, entity.Name, new Vector2((int)Position.X + (20 - entity.Name.Length - 1), (int)Position.Y - 12), Color.White); 

            if(entity.Level < 10)
                spriteBatch.DrawString(nameplateFont, entity.Level.ToString(), new Vector2((int)Position.X + 5, (int)Position.Y), Color.Black);
            else
                spriteBatch.DrawString(nameplateFont, entity.Level.ToString(), new Vector2((int)Position.X + 1, (int)Position.Y), Color.Black);
        }
    }
}
