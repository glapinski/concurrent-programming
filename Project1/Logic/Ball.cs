﻿using System;

namespace Logic
{
    public class Ball 
    {
        public double x { get; set; }
        public double y { get; set; }
        public double xS { get; set; }
        public double yS { get; set; }
        public double r { get; set; }

        Random rng = new Random();
        public double generateRandomDouble(double min, double max)
        {
            return rng.NextDouble() * (max - min) + min;
        }

        public Ball()
        {         
            x = generateRandomDouble(21, 479);
            y = generateRandomDouble(21, 479);

            xS = generateRandomDouble(1, 3);
            yS = generateRandomDouble(1, 3);

            r = 10;
        }

        public bool isCollision(Ball ball)
        {
            double distance = Math.Sqrt(Math.Pow(this.x - ball.x, 2) + Math.Pow(this.y - ball.y, 2));

            if (distance <= this.r + ball.r)
            {
                xS = ball.xS;
                yS = ball.yS;
                ball.x = xS;
                ball.y = yS;
                return true;
            }

            return false;
        }

        public void updatePosition(int axis)
        {
            double x2 = x + xS;
            double y2 = y + yS;

            if (x2 > axis-10 || x2 < 0)
            {
                xS = -xS;
            }
            if(y2 > axis-10 || y2 < 0)
            {
                yS = -yS;
            }

            x = x2;
            y = y2;
        }
    }
}
