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
        }

        [Test]
        public void createBallsTest()
        {
            BallRepository ballRepository = new BallRepository();

            ballRepository.CreateBalls(2);

            Assert.AreEqual(ballRepository.balls.Count, 2);
            Assert.AreEqual(ballRepository.balls[0].Id, 1);
            Assert.AreEqual(ballRepository.balls[1].Id, 2);
        }

        [Test]
        public void createRepoTest()
        {
            BallRepository ballRepository = new BallRepository();

            ballRepository.CreateBalls(2);

            Assert.AreEqual(ballRepository.GetBall(1), ballRepository.balls[0]);
            Assert.AreEqual(ballRepository.GetBall(2), ballRepository.balls[1]);
        }
    }
}
