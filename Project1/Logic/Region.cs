using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    public class Region
    {
        public int size { get; set; }

        public List<Ball> balls { get; set; }
        private Task changePosition;
        private int time = 30;


        public Region(int size)
        {
            this.size = size;
        }

        public void Execute()
        {
            changePosition = new Task(MoveBalls);
            changePosition.Start();
        }
        public void addBalls(int BallsNumber)
        {
            balls = new List<Ball>();
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
