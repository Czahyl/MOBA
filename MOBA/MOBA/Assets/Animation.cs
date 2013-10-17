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

namespace MOBA.Assets
{
    public class Animation
    {
        const float INCREMENT = 1;
        float SPEED = 1f;
        int DELAY;
        bool PAUSED = false;

        Main m;

        public List<Image> buffer;

        int index = 0;
        float i = 0;

        public Animation(int delay)
        {
            DELAY = delay;

            buffer = new List<Image>();
        }

        public void Run()
        {
            if (!PAUSED)
            {
                i += INCREMENT * SPEED;
                Debug.WriteLine(i);
            }

            if (i >= DELAY)
            {
                if (index < buffer.Count())
                    index++; // TEST FAGGOTS
                else
                    index = 0;

                i = 0;
            }
        }

        public Texture2D Frame()
        {
            return buffer[index].texture;
        }

        public Rectangle srcRect()
        {
            return buffer[index].sRect;
        }

        public void setSpeed(float speed)
        {
            SPEED = speed;
        }

        public void Start()
        {
            PAUSED = false;
        }

        public void Stop()
        {
            PAUSED = true;
        }
    }
}
