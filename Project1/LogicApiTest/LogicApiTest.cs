using NUnit.Framework;
using Logic;

namespace LogicTest
{
    internal class LogicAPITest
    {
        private LogicAPI testLogicAPI;
        [SetUp]
        public void Setup()
        {
            testLogicAPI = LogicAPI.CreateLayer();
        }
        /*[Test]
        public void getBallRadiusTest()
        {
            testLogicAPI.AddBallsAndStart(1);
        }*/
    }
}
