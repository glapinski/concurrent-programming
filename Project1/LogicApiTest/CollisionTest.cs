using Logic;
using NUnit.Framework;

namespace LogicTest
{
    public class CollisionTest
    {
        private Collision testCollision;
        [SetUp]
        public void Setup()
        {
            testCollision = new Collision(1, 1, 5, 5, 10, 10);
        }
        [Test]
        public void IsCollisionTest()
        {
            Assert.IsTrue(testCollision.IsCollision(21, 1, 10, true));
            Assert.IsTrue(testCollision.IsCollision(20, 1, 10, false));

            Assert.IsFalse(testCollision.IsCollision(26, 1, 10, true));
            Assert.IsFalse(testCollision.IsCollision(22, 1, 10, false));

        }

        [Test]
        public void IsTouchingBoundariesXandYTest()
        {
            Assert.IsTrue(testCollision.IsTouchingBoundariesX(3));
            Assert.IsTrue(testCollision.IsTouchingBoundariesY(3));

            Assert.IsFalse(testCollision.IsTouchingBoundariesX(6));
            Assert.IsFalse(testCollision.IsTouchingBoundariesY(6));
        }

    }
}
