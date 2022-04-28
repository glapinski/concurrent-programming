using NUnit.Framework;
using Logic;

namespace LogicApiTest
{
    internal class RegionTest
    {
        
        [SetUp]
        public void Setup()
        {
            Region region = new Region(700);
            region.addBalls(1);
            Assert.AreEqual(region.balls.Count, 1);
        }

        [Test]
        public void CreateBallsTest()
        {
            Region region = new Region(800);
            region.addBalls(3);

            double pX1 = region.balls[0].x;
            double pY1 = region.balls[0].y;

            double pX2 = region.balls[1].x;
            double pY2 = region.balls[1].y;

            region.MoveBall();

            Assert.AreNotEqual(region.balls[0].x, pX1);
            Assert.AreNotEqual(region.balls[0].y, pY1);
            Assert.AreNotEqual(region.balls[1].x, pX2);
            Assert.AreNotEqual(region.balls[1].y, pY2);
        }
    }
}