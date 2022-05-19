using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Data;

namespace DataTest
{
    internal class BallTest
    {
        private Ball testBall;
        [SetUp]
        public void Setup()
        {
            testBall = new Ball(1);
        }

        [Test]
        public void spawnBallTest()
        {
            Ball ball = new Ball(1);

            Assert.IsTrue(ball.PositionX <= 500 && ball.PositionX >= 1);
            Assert.IsTrue(ball.PositionY <= 500 && ball.PositionY >= 1);

            Assert.IsTrue(ball.MoveX <= 5 && ball.MoveX >= 2);
            Assert.IsTrue(ball.MoveY <= 5 && ball.MoveY >= 2);
        }

        [Test]
        public void changeBallPositionTest()
        {
            Ball ball = new Ball(1);

            double positionX = ball.PositionX;
            double positionY = ball.PositionY;
            ball.ChangeBallPosition();
            Assert.AreEqual(ball.PositionX, positionX + ball.MoveX);
            Assert.AreEqual(ball.PositionY, positionY + ball.MoveY);
        }
    }
}
