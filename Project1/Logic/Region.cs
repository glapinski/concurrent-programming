using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    internal class Region
    {
        public int size { get; set; }

        public List<Ball> balls { get; private set; }
        private Task changePosition;
        private int time = 30;


        public Region(int size)
        {
            this.size = size;
            balls = new List<Ball>();
        }

        public void Execute()
        {
            changePosition = new Task(MoveBalls);
            changePosition.Start();
        }
        public void addBalls(int BallsNumber)
        {
            for(int i = 0;i<BallsNumber ;i++)
            {
                balls.Add(new Ball());
            }
        }

        public void MoveBall()
        {
            foreach (Ball ball in balls)
            {
                ball.updatePosition(size);
            }
        }

        public void MoveBalls()
        {
            while (true)
            {
                MoveBall();
                Thread.Sleep(time);
            }
        }
    }
}
