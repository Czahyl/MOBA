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
using MOBA.Characters.Prototype;
using MOBA.Characters.Controller;

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

        public bool isVisible(BaseEntity e) // We have to revert the position from world coords to the camera offset
        {
            Vector2 rectPos = new Vector2(e.Bounds.X, e.Bounds.Y);

            rectPos = Vector2.Transform(rectPos, Main.Cam.Transform);

            for (int i = 0; i < emitters.Count; i++)
            {
                if (emitters[i].inCircle(rectPos) && emitters[i].layer >= e.visionLayer)
                    return true;
            }
            return false;
        }

        public bool shadeInLight(Rectangle rect, int lightLayer)
        {
            for (int i = 0; i < emitters.Count; i++)
            {
                if (emitters[i].inCircle(new Vector2(rect.X, rect.Y)) && emitters[i].layer <= lightLayer)
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
                        shades[i].Light(shadeInLight(shades[i].Rect(), emitters[j].layer));
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
                m.spriteBatch.Draw(Main.Assets.getTexture(0).Texture, shades[i].Rect(), new Color(50, 50, 50, shades[i].alpha));
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
            alpha = 100;
        }

        public void Light(bool isLight)
        {
            if (isLight)
                alpha = 0;
            else
                alpha = 100;
        }

        public Rectangle Rect()
        {
            return new Rectangle(x, y, 8, 8);
        }
    }
}
