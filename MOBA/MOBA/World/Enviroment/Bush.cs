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

            if (Rect.Contains(Main.controller.player.Center))
            {
                Main.controller.player.setAlpha(100);
                Main.controller.player.light.changeVisibility(1);
            }
            else
            {
                Main.controller.player.setAlpha(255);
                Main.controller.player.light.changeVisibility(-1);
            }

            foreach (MultiplayerController entity in Main.Players)
            {
                Player current = entity.player;

                if (Rect.Contains(current.Center))
                {
                    if (current.isFriendly()) // If friendly to the local player
                    {
                        current.setAlpha(100);
                        current.light.changeVisibility(1);
                    }
                    else
                        current.changeInvisibility(1);
                }
                else
                {
                    current.setAlpha(255);
                    current.light.changeVisibility(-1);
                }
            }

            foreach (MinionController entity in Main.Minions)
            {
                Minion current = entity.entity;

                if (Rect.Contains(current.Center))
                {
                    if (current.isFriendly()) // If friendly to the local player
                    {
                        current.setAlpha(100);
                        current.light.changeVisibility(1);
                    }
                    else
                    {
                        current.setAlpha(100);
                        current.changeVisibility(1);
                    }
                }
                else
                {
                    current.setAlpha(255);

                    current.changeVisibility(-1);
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
