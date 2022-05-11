using NUnit.Framework;
using Logic;

namespace LogicTest
{
    internal class RegionTest
    {
        private LogicAbstractApi _logicApi;
        [SetUp]
        public void Setup()
        {
            _logicApi = LogicAbstractApi.CreateApi();
        }

        [Test]
        public void CreateBallsTest()
        {
            /*Region region = new Region(800);
            region.addBalls(3);

            double pX1 = region.balls[0].x;
            double pY1 = region.balls[0].y;

            double pX2 = region.balls[1].x;
            double pY2 = region.balls[1].y;

            region.MoveBall();

            Assert.AreNotEqual(region.balls[0].x, pX1);
            Assert.AreNotEqual(region.balls[0].y, pY1);
            Assert.AreNotEqual(region.balls[1].x, pX2);
            Assert.AreNotEqual(region.balls[1].y, pY2);*/

            _logicApi.start();
        }
    }
}