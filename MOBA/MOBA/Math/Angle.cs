using System;
using System.Collections.Generic;

namespace MOBA.Math
{
    using Math = System.Math;

    public class Angle
    {
        public float Degrees
        { get; private set; }

        public float Radians
        { get { return (float)Math.PI * (Degrees / 180.0f); } set { } }

        public Angle(float deg)
        {
            Degrees = deg;
        }
    }
}