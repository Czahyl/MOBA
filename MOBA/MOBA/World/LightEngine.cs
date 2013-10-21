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
    public class LightEngine
    {
        protected Main m;
        public List<Shade> shades = new List<Shade>();
        private List<LightEmitter> emitters = new List<LightEmitter>();

        int timer;

        public LightEngine(Main main)
        {
            m = main;

            for (int x = 0; x < Main.WIDTH; x += 16)
            {
                for (int y = 0; y < Main.HEIGHT; y += 16)
                {
                    shades.Add(new Shade(x, y));
                }
            }
        }

        public void plugEmitter(LightEmitter e)
        {
            emitters.Add(e);
        }

        public void destroyEmitter(LightEmitter e)
        {
            emitters.Remove(e);
        }

        public bool checkAllEmitters(Rectangle rect, int lightLayer)
        {
            for (int i = 0; i < emitters.Count; i++)
            {
                if (emitters[i].inCircle(rect) && emitters[i].layer >= lightLayer)
                    return true;
            }
            return false;
        }

        public void Update()
        {
            timer++;

            if (timer >= 5)
            {
                for (int j = 0; j < emitters.Count; j++)
                {
                    for (int i = 0; i < shades.Count; i++)
                    {
                        if (emitters[j].inCircle(shades[i].rect()))
                            shades[i].Light(true);
                        else
                            shades[i].Light(checkAllEmitters(shades[i].rect(), emitters[j].layer));
                    }
                }
                timer = 0;
            }
        }

        public void Draw()
        {
            for(int i = 0; i < shades.Count; i++)
            {
                m.spriteBatch.Draw(Main.assets.getTexture(0).texture, shades[i].rect(), new Color(50, 50, 50, shades[i].alpha));
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
            return new Rectangle(x, y, 16, 16);
        }
    }
}
