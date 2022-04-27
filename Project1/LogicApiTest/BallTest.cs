using NUnit.Framework;
using Logic;

namespace LogicTest
{
    internal class BallTest
    {
        private BallAbstract testBall;
        [SetUp]
        public void Setup()
        {
            testBall = BallAbstract.CreateAPI(100, 100, 10, 10, 10);
        }

        [Test]
        public void UpdatePositionTest()
        {
            Assert.AreEqual(100, testBall.xPosition);
            Assert.AreEqual(100, testBall.yPosition);
            testBall.UpdatePosition();
            Assert.AreEqual(110, testBall.xPosition);
            Assert.AreEqual(110, testBall.yPosition);
        }

        [Test]
        public void ChangeDirectionTest()
        {
            Assert.AreEqual(100, testBall.xPosition);
            Assert.AreEqual(100, testBall.yPosition);
            testBall.ChangeDirection('x');
            testBall.UpdatePosition();
            Assert.AreEqual(90, testBall.xPosition);
            Assert.AreEqual(110, testBall.yPosition);
            testBall.ChangeDirection('y');
            Assert.AreEqual(80, testBall.xPosition);
            Assert.AreEqual(100, testBall.yPosition);
        }
    }
}
