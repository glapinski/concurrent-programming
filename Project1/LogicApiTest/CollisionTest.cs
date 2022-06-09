using Logic;
using Data;
using NUnit.Framework;
using System;

namespace LogicTest
{
    public class CollisionTest
    {
        public class Ball : IBall
        {
            public int Id { get; }

            public Ball(double x, double y, double moveX, double moveY, int radius)
            {
                PositionX = x;
                PositionY = y;
                Radius = radius;
                MoveX = moveX;
                MoveY = moveY;
            }

            public double PositionX { get; }
            public double PositionY { get; }

            public int Radius { get; }
            public double Mass { get; }

            public double SpeedX { get; set; }
            public double SpeedY { get; set; }

            public double MoveX { get; set; }
            public double MoveY { get; set; }

            public IDisposable Subscribe(IObserver<IBall> observer)
            {
                throw new NotImplementedException();
            }
        }
        private IBall testBall;
        [SetUp]
        public void Setup()
        {
            testBall = new Ball(1, 1, 5, 5, 10);
        }
        [Test]
        public void IsCollisionTest()
        {
            IBall testBall2 = new Ball(21, 1, 5, 5, 10);
            Assert.IsTrue(Collision.IsCollision(testBall, testBall2));

            testBall2 = new Ball(20, 1, 5, 5, 10);
            Assert.IsTrue(Collision.IsCollision(testBall, testBall2));

            testBall2 = new Ball(26, 1, 5, 5, 10);
            Assert.IsFalse(Collision.IsCollision(testBall, testBall2));
        }

        [Test]
        public void IsTouchingBoundariesXandYTest()
        {
            double speedX = testBall.SpeedX;
            double speedY = testBall.SpeedY;

            Collision.IsTouchingBoundaries(testBall, 100);

            Assert.AreEqual(-speedX, testBall.SpeedX);
            Assert.AreEqual(-speedY, testBall.SpeedY);
        }

    }
}
