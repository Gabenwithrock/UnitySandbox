using System.Threading;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Editor
{
    [TestFixture]
    public class UnityUnitTests {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Debug.Log("OneTimeSetUp");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Debug.Log("OneTimeTearDown");
        }
        
        [TestCase(12, 3)]
        public void Test1(int a, int b)
        {
            Assert.AreEqual(a / 4, b);
        }

        [Test]
        [SetCulture("ru-RU")]
        public void CultureTest()
        {
            
        }

        [Test]
        // [Timeout(2000)] // NOT WORKING
        [MaxTime(2000)] // doesn't stop if exceed
        public void LongOperation()
        {
            //while (true){}
            Thread.Sleep(3000);
        }

        [Test]
        public void TestRange([NUnit.Framework.Range(0.2,0.6,0.2)] double d)
        {
            
        }
        
        [Test]
        public void TestValues([Values(1,2,3,4,5)] double d)
        {
            
        }
    }
}
