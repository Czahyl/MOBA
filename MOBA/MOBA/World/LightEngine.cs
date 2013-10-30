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
using MOBA.Math;

namespace MOBA.World
{
    public class LightEngine
    {
        protected Main m;
        public List<Shade> shades = new List<Shade>();
        private List<LightEmitter> emitters = new List<LightEmitter>();
        private List<LightEmitter> fadeEmitter = new List<LightEmitter>();

        private Timer lag;

        public LightEngine(Main main, int width, int height)
        {
            m = main;

            for (int x = 0; x < width; x += 8)
            {
                for (int y = 0; y < height; y += 8)
                {
                    shades.Add(new Shade(x, y));
                }
            }

            lag = new Timer(5, true);
        }

        public void plugEmitter(LightEmitter e)
        {
            emitters.Add(e);
        }

        public void destroyEmitter(LightEmitter e)
        {
            fadeEmitter.Add(e);
        }

        public bool inLight(Rectangle rect, int lightLayer)
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
            lag.Run();

            if (lag.Tick)
            {
                for (int j = 0; j < emitters.Count; j++)
                {
                    for (int i = 0; i < shades.Count; i++)
                    {
                        if (emitters[j].inCircle(shades[i].Rect()))
                            shades[i].Light(true);
                        else
                            shades[i].Light(inLight(shades[i].Rect(), emitters[j].layer));
                    }
                }
            }

            for (int i = 0; i < fadeEmitter.Count; i++)
            {
                if (fadeEmitter[i].r <= 0)
                {
                    emitters.Remove(fadeEmitter[i]);
                    fadeEmitter.RemoveAt(i);
                }
                else
                    fadeEmitter[i].Fade();
            }
        }

        public void Draw()
        {
            for(int i = 0; i < shades.Count; i++)
            {
                m.spriteBatch.Draw(Main.assets.getTexture(0).Texture, shades[i].Rect(), new Color(50, 50, 50, shades[i].alpha));
            }
        }
    }

    public class Shade
    {
        int x, y;

        public int alpha;

        public Shade(int X, int Y)
        {
            x = X;
            y = Y;
            alpha = 175;
        }

        public void Light(bool isLight)
        {
            if (isLight)
                alpha = 0;
            else
                alpha = 175;
        }

        public Rectangle Rect()
        {
            return new Rectangle(x, y, 8, 8);
        }
    }
}
