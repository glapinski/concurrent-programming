using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Data
{
    internal class Ball
    {
        public int id { get; }
        public double x { get; set; }
        public double y { get; set; }
        public double xS { get; set; }
        public double yS { get; set; }
        public double r { get; set; }
        public double m { get; }
        private Thread positionUpdater;
        public int regionSize { get; set; } = 100;

        Random rng = new Random();
        public double generateRandomDouble(double min, double max)
        {
            return rng.NextDouble() * (max - min) + min;
        }

        public Ball(int id)
        {
            this.id = id;
            x = generateRandomDouble(21, 479);
            y = generateRandomDouble(21, 479);

            xS = generateRandomDouble(1, 3);
            yS = generateRandomDouble(1, 3);

            r = 10;
            m = 10;
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

        private void MoveBall()
        {
            while(true)
            {
                updatePosition(regionSize);
            }
        }

        public void updatePosition(int axis)
        {
            double x2 = x + xS;
            double y2 = y + yS;

            if (x2 > axis-10 || x2 < 0)
            {
                xS = -xS;
            }
            if (y2 > axis-10 || y2 < 0)
            {
                yS = -yS;
            }

            x = x2;
            y = y2;
        }

        public void StartMove()
        {
            this.positionUpdater = new Thread(this.MoveBall);
            positionUpdater.Start();
        }
    }
}
