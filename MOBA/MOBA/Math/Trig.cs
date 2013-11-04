using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MOBA.Math
{
    using Math = System.Math;

    public class Trig
    {
        public double Hypotenuse(double side1, double side2)
        {
            return System.Math.Sqrt(side1 * side1 + side2 * side2);
        }

        public bool inRadius(int centerX, int centerY, int r, int rectX, int rectY)
        {
            return Hypotenuse(centerX - rectX, centerY - rectY) <= r;
        }

        public float degreesToRadians(float deg)
        {
            return (float)Math.PI * (deg / 180.0f);
        }

        public float radiansToDegrees(float radians)
        {
            return (float)(radians * (180 / Math.PI));
        }

        public Angle angleBetweenPoints(Vector2 pointA, Vector2 pointB)
        {
            return new Angle((float)Math.Atan2(pointB.Y - pointA.Y, pointB.X - pointA.X) * (float)(180 / Math.PI));
        }

        public Angle angleBetweenPoints(Point pointA, Point pointB)
        {
            return new Angle((float)Math.Atan2(pointB.Y - pointA.Y, pointB.X - pointA.X) * (float)(180 / Math.PI));
        }
    }
}