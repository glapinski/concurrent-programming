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
        public void createRepoTest()
        {
            Assert.IsNotNull(testBallRepository);
            Assert.IsEmpty(testBallRepository.getBallList());
            testBallRepository.CreateBalls(2);
            Assert.IsNotEmpty(testBallRepository.getBallList());
            Assert.IsNotNull(testBallRepository.getBall(1));
            Assert.IsNotNull(testBallRepository.getBall(2));
        }
    }
}
