using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Data;

namespace DataTest
{
    public class BallTest
    {

        [Test]
        public void ChangeBallPositionTest()
        {
            Ball ball = new Ball(1);

            double positionX = ball.PositionX;
            double positionY = ball.PositionY;
            ball.ChangeBallPosition(0);
            Assert.AreEqual(ball.PositionX, positionX + ball.SpeedX / 5);
            Assert.AreEqual(ball.PositionY, positionY + ball.SpeedY / 5);

            positionX = ball.PositionX;
            positionY = ball.PositionY;
            ball.ChangeBallPosition(2);
            Assert.AreEqual(ball.PositionX, positionX + (ball.SpeedX / 5) * 2);
            Assert.AreEqual(ball.PositionY, positionY + (ball.SpeedY / 5) * 2);
        }

        [Test]
        public void RandomPositionAndMoveTest()
        {
            Ball ball = new Ball(1);

            Assert.IsTrue(ball.PositionX <= 500 && ball.PositionX >= 1);
            Assert.IsTrue(ball.PositionY <= 500 && ball.PositionY >= 1);

        }
    }
}
