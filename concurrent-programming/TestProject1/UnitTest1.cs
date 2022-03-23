using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            class1.Calculator x = new class1.Calculator();
            int y = x.Add(2, 1);
            Assert.AreEqual(3, y);
        }
    }
}