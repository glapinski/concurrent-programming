using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Data;

namespace DataTest
{
    internal class BallRepositoryTest
    {
        private BallRepository testBallRepository;
        [SetUp]
        public void Setup()
        {
            testBallRepository = new BallRepository();
        }

        [Test]
        public void createBallsTest()
        {
            testBallRepository.CreateBalls(2);

            Assert.AreEqual(testBallRepository.balls.Count, 2);
            Assert.AreEqual(testBallRepository.balls[0].Id, 1);
            Assert.AreEqual(testBallRepository.balls[1].Id, 2);
        }

        [Test]
        public void getBallTest()
        {
            testBallRepository.CreateBalls(2);

            Assert.AreEqual(testBallRepository.GetBall(1), testBallRepository.balls[0]);
            Assert.AreEqual(testBallRepository.GetBall(2), testBallRepository.balls[1]);
        }
    }
}
