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
            testBall = new Ball(0);
        }

        [Test]
        public void spawnBallTest()
        {
            Assert.IsTrue(testBall.x <= 479 && testBall.x >= 21);
            Assert.IsTrue(testBall.y <= 479 && testBall.y >= 21);

            Assert.IsTrue(testBall.xS <= 3 && testBall.xS >= 1);
            Assert.IsTrue(testBall.yS <= 3 && testBall.yS >= 1);
        }
    }
}
