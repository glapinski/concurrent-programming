using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        [Test]
        public void getBallRadiusTest()
        {
            testLogicAPI.AddBallsAndStart(1);

            Assert.AreEqual(testLogicAPI.getBallRadius(1), 15);
        }


    }
}
