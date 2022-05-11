using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    internal class BallRepository
    {
        private List<Ball> balls;

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
            foreach (Ball ball in balls)
            {
                if (ball.id == ballId)
                {
                    return ball;
                }
            }
            return null;
        }
    }
}
