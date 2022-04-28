﻿using NUnit.Framework;
using Logic;

namespace LogicTest
{
    internal class BallTest
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void UpdatePositionTest()
        {
            Ball ball = new Ball();
            float positionX = ball.x;
            float positionY = ball.y;
            ball.updatePosition(530);
            Assert.AreEqual(ball.x, positionX + ball.xS);
            Assert.AreEqual(ball.y, positionY + ball.yS);
        }
    }
}
