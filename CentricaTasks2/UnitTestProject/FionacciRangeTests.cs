using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FibancciService.FibonacciAlgorithm;
using System.Numerics;

namespace UnitTestProject
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class FionacciRangeTests
    {
        public FionacciRangeTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GeneraFibonacciPosRangeTest()
        {
            GenerateFibonacciPosRange fr = new GenerateFibonacciPosRange();
            BigInteger[] res = fr.GetSeriesBetweenPosition(40, 50);
            Assert.AreEqual(res.Length, 11);//12586269025,{4807526976} +		

            Assert.AreEqual(res[10].ToString(), "12586269025");

        }

        [TestMethod]
        public void GeneraFibonacciPosRangeWithLargePositionTest()
        {
            GenerateFibonacciPosRange fr = new GenerateFibonacciPosRange();
            BigInteger[] res = fr.GetSeriesBetweenPosition(4000, 5000);
            Assert.AreEqual(res.Length, 1001);
            var t = res[1].ToString();
            Assert.AreEqual(res[1000].ToString(), "3878968454388325633701916308325905312082127714646245106160597214895550139044037097010822916462210669479293452858882973813483102008954982940361430156911478938364216563944106910214505634133706558656238254656700712525929903854933813928836378347518908762970712033337052923107693008518093849801803847813996748881765554653788291644268912980384613778969021502293082475666346224923071883324803280375039130352903304505842701147635242270210934637699104006714174883298422891491273104054328753298044273676822977244987749874555691907703880637046832794811358973739993110106219308149018570815397854379195305617510761053075688783766033667355445258844886241619210553457493675897849027988234351023599844663934853256411952221859563060475364645470760330902420806382584929156452876291575759142343809142302917491088984155209854432486594079793571316841692868039545309545388698114665082066862897420639323438488465240988742395873801976993820317174208932265468879364002630797780058759129671389634214252579116872755600360311370547754724604639987588046985178408674382863125");

        }

        [TestMethod]
        public void GeneraFibonacciPosRangeWithCacheTest()
        {
            GenerateFibonacciPosRange fr = new GenerateFibonacciPosRange();
            BigInteger[] res = fr.GetSeriesBetweenPosition(4000, 5000);
            Assert.AreEqual(res.Length, 1001);
            Assert.AreEqual(res[1000].ToString(), "3878968454388325633701916308325905312082127714646245106160597214895550139044037097010822916462210669479293452858882973813483102008954982940361430156911478938364216563944106910214505634133706558656238254656700712525929903854933813928836378347518908762970712033337052923107693008518093849801803847813996748881765554653788291644268912980384613778969021502293082475666346224923071883324803280375039130352903304505842701147635242270210934637699104006714174883298422891491273104054328753298044273676822977244987749874555691907703880637046832794811358973739993110106219308149018570815397854379195305617510761053075688783766033667355445258844886241619210553457493675897849027988234351023599844663934853256411952221859563060475364645470760330902420806382584929156452876291575759142343809142302917491088984155209854432486594079793571316841692868039545309545388698114665082066862897420639323438488465240988742395873801976993820317174208932265468879364002630797780058759129671389634214252579116872755600360311370547754724604639987588046985178408674382863125");
            res = fr.GetSeriesBetweenPosition(20, 30);
            Assert.AreEqual(res[10].ToString(), "832040");
        }
    }
}
