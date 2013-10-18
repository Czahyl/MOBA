using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MOBA.Math
{
    using Math = System.Math;

    public static class Trig
    {
        public static bool pointInCircle(int x, int y, int centerX, int centerY, int radius)
        {
            return (((x - centerX) ^ 2) + ((y - centerY) ^ 2)) < (radius ^ 2);
        }

        public static bool pointInCricle(Vector2 point, Vector2 center, int radius)
        {
            return (((int)(point.X - center.X) ^ 2) + ((int)(point.X - center.Y) ^ 2)) < (radius ^ 2);
        }

        public static bool pointInCircle(Point point, Point center, int radius)
        {
            return (((point.X - center.X) ^ 2) + ((point.X - center.Y) ^ 2)) < (radius ^ 2);
        }

        public static float degreesToRadians(float deg)
        {
            return (float)Math.PI * (deg / 180.0f);
        }

        public static float radiansToDegrees(float radians)
        {
            return (float)(radians * (180 / Math.PI));
        }

        public static Angle angleBetweenPoints(Vector2 pointA, Vector2 pointB)
        {
            return new Angle((float)Math.Atan2(pointB.Y - pointA.Y, pointB.X - pointA.X) * (float)(180 / Math.PI));
        }

        public static Angle angleBetweenPoints(Point pointA, Point pointB)
        {
            return new Angle((float)Math.Atan2(pointB.Y - pointA.Y, pointB.X - pointA.X) * (float)(180 / Math.PI));
        }
    }
}