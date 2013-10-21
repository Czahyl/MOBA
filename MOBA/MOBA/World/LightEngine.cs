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
using System.Diagnostics;

namespace MOBA.World
{
    public static class LightEngine
    {
        private static Main m;
        public static List<Shade> shades = new List<Shade>();

        public static void Init(Main main)
        {
            m = main;

            for (int x = 0; x < Main.WIDTH; x += 5)
            {
                for (int y = 0; y < Main.HEIGHT; y += 5)
                {
                    shades.Add(new Shade(x, y));
                }
            }
        }

        public static void Draw()
        {
            for(int i = 0; i < shades.Count; i++)
            {
                m.spriteBatch.Draw(m.assets.getTexture(0).texture, shades[i].rect(), new Color(50, 50, 50, shades[i].alpha));
            }
        }
    }

    public class Shade
    {
        int x, y;
        public int alpha { get; private set; }

        public Shade(int X, int Y)
        {
            x = X;
            y = Y;
            alpha = 225;
        }

        public void Light(bool isLight)
        {
            if (isLight)
                alpha = 0;
            else
                alpha = 225;
        }

        public Rectangle rect()
        {
            return new Rectangle(x, y, 5, 5);
        }
    }
}
