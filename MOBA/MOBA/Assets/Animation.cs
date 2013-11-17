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
using MOBA.Input;

namespace MOBA.Assets
{
    public class Animation
    {
        const float INCREMENT = 1;
        float SPEED = 1f;
        int DELAY;
        bool PAUSED = false;

        public List<Image> buffer;

        int index = 0;
        float i = 0;

        public Animation(int delay)
        {
            DELAY = delay;

            buffer = new List<Image>();
        }

        public Image Frame()
        {
            if (!PAUSED)
            {
                i += INCREMENT * SPEED;
            }

            if (i >= DELAY)
            {
                if (index < buffer.Count() - 1)
                    index++;
                else
                    index = 0;

                i = 0;
            }

            return buffer[index];
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
