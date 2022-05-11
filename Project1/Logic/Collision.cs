using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    internal static class Collision
    {
        public static bool IsCollision(double x1, double y1, double x2, double y2, double r1, double r2)
        {
            double distance = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));

            if (distance <= r1 + r2)
            {
                return true;
            }

            return false;
        }
    }
}
