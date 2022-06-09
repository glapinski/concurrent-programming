using NUnit.Framework;
using Data;

namespace DataTest
{
    public class DataAPITest
    {
        [Test]
        public void CreateBallsTest()
        {
            DataAbstractAPI dataApi = DataAbstractAPI.CreateDataApi();

            dataApi.createBalls(2);

            Assert.AreEqual(dataApi.getBallsAmount(), 2);
        }
    }
}