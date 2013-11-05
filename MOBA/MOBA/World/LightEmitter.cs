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
using MOBA.Characters.Prototype;

namespace MOBA.World
{
    public class LightEmitter
    {
        private LightEngine e;
        public BaseEntity entity;
        public Vector2 pos
        { get; private set; }
        public int layer 
        { get; private set; }
        public float r
        { get; private set; }

        public LightEmitter(LightEngine engine, BaseEntity boundEntity, float radius, int lightLayer)
        {
            e = engine;
            entity = boundEntity;

            pos = entity.Position;
            r = radius;
            layer = lightLayer;
        }

        public LightEmitter()
        {

        }

        public void Update()
        {
            pos = entity.Position;
            pos = Vector2.Transform(pos, Main.Cam.Transform);
        }

        public void Destroy()
        {
            e.destroyEmitter(this);
        }

        public void Fade()
        {
            r -= 4;
        }

        public bool inCircle(Vector2 point)
        {
            return Main.Trig.inRadius((int)pos.X, (int)pos.Y, (int)r, (int)point.X, (int)point.Y);
        }
    }
}
