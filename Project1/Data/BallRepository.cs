using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    internal class BallRepository
    {
        public List<Ball> balls { get; set; }

        public int BoardSize { get; private set; } = 515;

        public BallRepository()
        {
            balls = new List<Ball>();
        }

        public void CreateBalls(int ballsAmount)
        {
            for (int i = 0; i < ballsAmount; i++)
            {
                balls.Add(new Ball(i + 1));
            }
        }

        public Ball getBall(int ballId)
        {
            if (balls[ballId - 1] != null)
            {
                return balls[ballId - 1];
            }
            return null;
        }
        /*public List<Ball> getBallList()
        {
            return balls;
        }*/
    }
}
