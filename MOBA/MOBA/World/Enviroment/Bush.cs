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
        public Bush(Vector2 pos)
        {
            Position = pos;
        }

        public override void Update()
        {
            Rect = new Rectangle((int)Position.X, (int)Position.Y, 64, 32);

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

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
