using NUnit.Framework;
using Data;

namespace DataTest
{
    internal class DataApiTest
    {
        private DataAbstractAPI testDataApi;
        [SetUp]
        public void Setup()
        {
            testDataApi = DataAbstractAPI.CreateDataApi();
        }

        [Test]
        public void createBallsTest()
        {

            testDataApi.createBalls(2);

            Assert.AreEqual(testDataApi.getBallsAmount(), 2);
        }

        [Test]
        public void ballsSpeedTest()
        {
            testDataApi.createBalls(1);

            testDataApi.setBallSpeed(1, 2, 2);

            Assert.AreEqual(testDataApi.getBallSpeedX(1), 2);
            Assert.AreEqual(testDataApi.getBallSpeedY(1), 2);
        }
    }
}