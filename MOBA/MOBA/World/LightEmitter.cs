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
    public class LightEmitter
    {
        private LightEngine e;
        int x, y;
        float r;
        bool locked = false;
        public int layer { get; private set; } 

        public LightEmitter(LightEngine engine, int X, int Y, float radius, bool isLocked, int lightLayer)
        {
            e = engine;

            x = X;
            y = Y;
            r = radius;
            locked = isLocked;
            layer = lightLayer;
        }

        public LightEmitter()
        {

        }

        public void Destroy()
        {
            e.destroyEmitter(this);
        }

        public bool inCircle(Rectangle rect)
        {
            if (!locked) return Math.Trig.inRadius(x + Camera.X, y + Camera.Y, (int)r, rect.X, rect.Y);
            else return Math.Trig.inRadius(x, y, (int)r, rect.X, rect.Y);
        }
    }
}
