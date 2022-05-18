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
            Assert.IsTrue(testBall.x <= 500 && testBall.x >= 1);
            Assert.IsTrue(testBall.y <= 500 && testBall.y >= 1);

            Assert.IsTrue(testBall.xS <= 5 && testBall.xS >= 2);
            Assert.IsTrue(testBall.yS <= 5 && testBall.yS >= 2);
        }

        [Test]
        public void changeBallPositionTest()
        {
            double positionX = testBall.x;
            double positionY = testBall.y;
            testBall.ChangeBallPosition();
            Assert.AreEqual(testBall.x, positionX+testBall.xS);
            Assert.AreEqual(testBall.y, positionY+testBall.yS);
        }
    }
}
