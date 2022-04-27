using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public abstract uint screen_width { get;  }
        public abstract uint screen_height { get; }
        public abstract void createBalls(uint count);

    }

    internal class LogicApi : LogicAbstractAPI
    {
        public override uint screen_width { get; }

        public override uint screen_height { get; }
        private List<Ball> balls;
        public List<Ball> Balls
        {
            get { return balls; }
        }

        public LogicApi(uint width, uint height)
        {
            screen_width = width;
            screen_height = height;
            balls = new List<Ball>();
        }

        public override void createBalls(uint count)
        {
            Random random = new Random();
            for (uint i = 0; i < count; i++)
            {
                float random_xPosition = random.Next(0, (int)screen_width - 10);
                float random_yPosition = random.Next(0, (int)screen_height - 10);

                float random_xSpeed = random.Next(10);
                float random_ySpeed = random.Next(10);

                balls.Add(new Ball(random_xPosition, random_yPosition, random_xSpeed, random_ySpeed, 10));
            }
        }

        
    }
}
