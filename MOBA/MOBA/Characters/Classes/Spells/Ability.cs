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
using MOBA.Assets;
using MOBA.Characters.Controller;
using MOBA.Characters.Classes;
using MOBA.Characters.Prototype;
using MOBA.World;
using MOBA.Input;
using MOBA.Math;

namespace MOBA.Characters.Classes.Spells
{
    public class Ability
    {
        public Player pClass;

        public static Ability None;

        public Image image
        { get; protected set; }

        public int Damage
        { get; protected set; }

        public Vector2 Direction
        { get; protected set; }

        public Rectangle Rect
        { get; protected set; }

        public Rectangle hitRect
        { get; protected set; }

        public bool Alive = false;

        public Vector2 Position;

        public Timer drawTime;
        public Timer cooldown;

        public Ability(Player player)
        {
            pClass = player;
        }

        public virtual void Cast(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(image.Texture, new Rectangle((int)Position.X, (int)Position.Y, Rect.Width, Rect.Height), image.sRect, Color.White);
        }
    }
}
