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

            Assert.IsTrue(testBall.PositionX <= 500 && testBall.PositionX >= 1);
            Assert.IsTrue(testBall.PositionY <= 500 && testBall.PositionY >= 1);

            Assert.IsTrue(testBall.MoveX <= 5 && testBall.MoveX >= 2);
            Assert.IsTrue(testBall.MoveY <= 5 && testBall.MoveY >= 2);
        }

        [Test]
        public void changeBallPositionTest()
        {
            double positionX = testBall.PositionX;
            double positionY = testBall.PositionY;
            testBall.ChangeBallPosition();
            Assert.AreEqual(testBall.PositionX, positionX + testBall.MoveX);
            Assert.AreEqual(testBall.PositionY, positionY + testBall.MoveY);
        }
    }
}
