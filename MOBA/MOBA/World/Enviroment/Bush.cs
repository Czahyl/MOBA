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

namespace MOBA.World.Enviroment
{
    public class Bush : WorldObject
    {
        public Bush(Vector2 pos) : base()
        {
            Position = pos;
        }

        public override void Update()
        {
            Rect = new Rectangle((int)Position.X, (int)Position.Y, 64, 64);

            foreach (PlayerController entity in Main.Players)
            {
                Player current = entity.entity;

                if (current.Bounds.Intersects(Rect))
                {
                    current.changeVisibility(1);
                }
                else
                {
                    current.changeVisibility(-1); // -1 changes the visibility to default
                }
            }

            foreach (MinionController entity in Main.Minions)
            {
                Minion current = entity.entity;

                if (Rect.Contains(current.Center))
                {
                    if (current.Friendly) // If friendly to the local player
                        current.setAlpha(100);
                    else
                        current.changeVisibility(1);
                }
                else
                {
                    current.setAlpha(255);

                    if(!current.Friendly)
                        current.changeVisibility(0);
                }
            }

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Main.Assets.getTexture(0).Texture, Rect, Color.Green);
            base.Draw(spriteBatch);
        }
    }
}
