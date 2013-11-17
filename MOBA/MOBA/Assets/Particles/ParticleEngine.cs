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
    public struct ParticlePreset
    {
        public int burstCount;
        public int minVel, maxVel;
        public Timer liveTime;
        public float burstRadius;

        public ParticlePreset(int burstCount, int minVel, int maxVel, Timer liveTime, float burstRadius)
        {
            this.burstCount = burstCount;
            this.minVel = minVel;
            this.maxVel = maxVel;
            this.liveTime = liveTime;
            this.burstRadius = burstRadius;
        }
    }

    public class ParticleEngine
    {
        Main m;

        public Dictionary<string, ParticlePreset> presets = new Dictionary<string,ParticlePreset>();
        private List<Particle> particleList = new List<Particle>();

        public ParticleEngine(Main main)
        {
            m = main;
        }

        public void removeParticle(Particle p)
        {
            particleList.Remove(p);
        }

        public void EmitParticles(Vector2 position, ParticlePreset preset)
        {
            for (int i = 0; i < preset.burstCount; i++)
            {
                particleList.Add(new Particle(this, position, preset));
            }
        }

        public void Update()
        {
            for (int i = 0; i < particleList.Count; i++)
                particleList[i].Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particleList.Count; i++)
                particleList[i].Draw(spriteBatch);
        }
    }
}
