using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MOBA.Math
{
    public class Timer
    {
        private float time;
        private float delay;
        public bool Tick { get; private set; }
        public int tickCount { get; private set; }
        bool stopped = false;

        public Timer(float Time, bool Millisecond)
        {
            if (Millisecond)
                delay = Time;
            else
                delay = Time * 60;

            stopped = false;
        }

        public Timer()
        {

        }

        public void Run()
        {
            if (!stopped)
                time++;

            if (time >= delay)
            {
                Tick = true;
                tickCount++;
                time = 0;
            }
            else
                Tick = false;
        }

        public void Stop()
        {
            stopped = true;
        }

        public void Resume()
        {
            stopped = false;
        }
    }
}
