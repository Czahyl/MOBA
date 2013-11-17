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
using MOBA.Math;

namespace MOBA.Assets.Particles
{
    public class Particle
    {
        ParticleEngine e;
        ParticlePreset settings;
        Vector2 pos, dir;
        private int vel;
        private Timer liveTime;

        public Particle(ParticleEngine engine, Vector2 startPos, ParticlePreset preset)
        {
            e = engine;
            settings = preset;
            pos = startPos;
            dir = PickRandomDirection();
            vel = Main.random.Next(settings.minVel, settings.maxVel);

            liveTime = settings.liveTime;
        }

        public void Update()
        {
            liveTime.Run();
            if (liveTime.Tick)
                e.removeParticle(this);
            pos += dir * vel;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Main.Assets.getTexture(3).Texture, new Rectangle((int)pos.X, (int)pos.Y, 10, 10), Color.White);
        }

        public Vector2 PickRandomDirection()
        {
            float angle = Main.RandomBetween(0, settings.burstRadius);
            return new Vector2((float)System.Math.Cos(angle), (float)System.Math.Sin(angle));
        }
    }
}
