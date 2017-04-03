using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FibancciService.FibonacciAlgorithm;
using System.Numerics;

namespace UnitTestProject
{
    [TestClass]
    public class FibonacciUnitTests
    {
        [TestMethod]
        public void FibonacciCalcNumAtPosTest()
        {
            FibonacciCalcNumAtPos fr = new FibonacciCalcNumAtPos();
            BigInteger res = fr.GetNumberAtPosition(10);
            Assert.AreEqual(res, 55);
            res = fr.GetNumberAtPosition(30);

        }

        [TestMethod]
        public void FibonacciCalcNumAtPosNegativeTest()
        {
            FibonacciCalcNumAtPos fr = new FibonacciCalcNumAtPos();
            BigInteger res = fr.GetNumberAtPosition(0);
            Assert.AreEqual(res, 0);
            res = fr.GetNumberAtPosition(-1);
            Assert.AreEqual(res, -1);
            res = fr.GetNumberAtPosition(1);
            Assert.AreEqual(res, 1);
            res = fr.GetNumberAtPosition(2);
            Assert.AreEqual(res, 1);
        }

        [TestMethod]
        public void FibonacciCalcNumAtPosWithLargestPositionTest()
        {
            FibonacciCalcNumAtPos fr = new FibonacciCalcNumAtPos();
            BigInteger res = fr.GetNumberAtPosition(100000);

            res = fr.GetNumberAtPosition(100);
            Assert.AreEqual(res.ToString(), "354224848179261915075");

        }


        [TestMethod]
        public void FibonacciCalcNumAtPosCacheTest()
        {
            FibonacciCalcNumAtPos fr = new FibonacciCalcNumAtPos();
            BigInteger res = fr.GetNumberAtPosition(100000);
            //see if the cache is geeting used.
            res = fr.GetNumberAtPosition(100);
            Assert.AreEqual(res.ToString(), "354224848179261915075");

        }
    }
}
