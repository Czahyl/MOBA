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
        bool stopped = false;

        public Timer(float Seconds)
        {
            delay = Seconds * 60;
            stopped = false;
        }

        public void Run()
        {
            if (!stopped)
                time++;

            if (time >= delay)
            {
                Tick = true;
                time = 0;
            }
            else
                Tick = false;
        }

        public void Stop()
        {
            stopped = true;
        }
    }
}
