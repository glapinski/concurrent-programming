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
           DataAbstractAPI dataapi = DataAbstractAPI.CreateAPI();

           dataapi.createBalls(2);

            Assert.AreEqual(dataapi.getBallsAmount(), 2);
        }

        [Test]
        public void ballsSpeedTest()
        {
            DataAbstractAPI dataapi = DataAbstractAPI.CreateAPI();

            dataapi.createBalls(1);

            dataapi.setBallSpeed(1, 2, 2);

            Assert.AreEqual(dataapi.getBallXSpeed(1), 2);
            Assert.AreEqual(dataapi.getBallYSpeed(1), 2);
        }
    }
}