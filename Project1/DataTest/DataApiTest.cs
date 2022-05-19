using NUnit.Framework;
using Data;

namespace DataTest
{
    internal class DataApiTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void createBallsTest()
        {
           DataAbstractAPI dataapi = DataAbstractAPI.CreateDataApi();

           dataapi.createBalls(2);

            Assert.AreEqual(dataapi.getBallsAmount(), 2);
        }

        [Test]
        public void ballsSpeedTest()
        {
            DataAbstractAPI dataapi = DataAbstractAPI.CreateDataApi();

            dataapi.createBalls(1);

            dataapi.setBallSpeed(1, 2, 2);

            Assert.AreEqual(dataapi.getBallSpeedX(1), 2);
            Assert.AreEqual(dataapi.getBallSpeedY(1), 2);
        }
    }
}