using NUnit.Framework;
using Logic;

namespace LogicApiTest
{
    internal class LogicApiTest
    {
        private LogicAbstractApi testApi;
        [SetUp]
        public void Setup()
        {
            testApi = LogicAbstractApi.CreateApi(500, 500);
        }

        [Test]
        public void CreateBallsTest()
        {
            Assert.AreEqual(0, testApi.GetBalls().Count);
            testApi.createBalls(5);
            Assert.AreEqual(5, testApi.GetBalls().Count);
        }
    }
}