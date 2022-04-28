using System;

namespace Logic
{
    public class Ball 
    {
        public float x { get; set; }
        public float y { get; set; }
        public float xS { get; set; }
        public float yS { get; set; }
        public float r { get; set; }

        public Ball()
        {
            Random rng = new Random();
            x = rng.Next(1, 500);
            y = rng.Next(1, 500);

            xS = rng.Next(1, 5);
            yS = rng.Next(1, 5);

            r = 10;
        }

        public void updatePosition(int axis)
        {
            float x2 = x + xS;
            float y2 = y + yS;

            if (x2 > axis-r || x < 0)
            {
                xS = -xS;
            }
            if(y2 > axis-r || y < 0)
            {
                yS = -yS;
            }
        }
    }
}
