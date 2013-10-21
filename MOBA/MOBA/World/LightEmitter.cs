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
        int x, y;
        float r;

        public LightEmitter(int X, int Y, float radius)
        {
            x = X;
            y = Y;
            r = radius;
        }

        public void Update()
        {
            for (int i = 0; i < LightEngine.shades.Count; i++)
            {
                LightEngine.shades[i].Light(Math.Trig.inRadius(x, y, (int)r, LightEngine.shades[i].rect().X, LightEngine.shades[i].rect().Y));
            }
        }
    }
}
