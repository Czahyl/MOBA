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
        Vector2 pos;
        float r;
        public int layer { get; private set; } 

        public LightEmitter(LightEngine engine, Vector2 position, float radius, int lightLayer)
        {
            e = engine;

            pos = position;
            r = radius;
            layer = lightLayer;
        }

        public LightEmitter()
        {

        }

        public void Update(Vector2 position)
        {
            pos = position;
        }

        public void Destroy()
        {
            e.destroyEmitter(this);
        }

        public bool inCircle(Rectangle rect)
        {
            return Math.Trig.inRadius((int)pos.X + Camera.X, (int)pos.Y + Camera.Y, (int)r, rect.X, rect.Y);
        }
    }
}
