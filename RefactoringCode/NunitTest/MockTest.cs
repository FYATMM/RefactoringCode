using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace NunitTest
{
    [TestFixture]
    public class MockTest
    {
        public class CalculatorTester
        {
            // 定义mock的逻辑
            private IUSD_RMB_ExchangeRateFeed prvGetMockExchangeRateFeed()
            {
                Mock<IUSD_RMB_ExchangeRateFeed> mockObject = new Mock<IUSD_RMB_ExchangeRateFeed>();
                mockObject.Setup(m => m.GetActualUSDValue()).Returns(500);
                return mockObject.Object;
            }
            // 测试divide方法
            [Test(Description = "Divide 9 by 3. Expected result is 3.")]
            public void TC1_Divide9By3()
            {
                IUSD_RMB_ExchangeRateFeed feed = this.prvGetMockExchangeRateFeed();
                ICalculator calculator = new Calculator(feed);
                int actualResult = calculator.Divide(9, 3);
                int expectedResult = 3;
                Assert.AreEqual(expectedResult, actualResult);
            }
            [Test(Description = "Divide any number by zero. Should throw an System.DivideByZeroException exception.")]
            ////[ExpectedException(typeof(System.DivideByZeroException))]  //过时了，用assert.throws代替
            public void TC2_DivideByZero()
            {
                IUSD_RMB_ExchangeRateFeed feed = prvGetMockExchangeRateFeed();
                ICalculator calculator = new Calculator(feed);
                int actualResult = calculator.Divide(9, 0);
            }
            [Test()]
            public void AssertThrowTest()
            {
                IUSD_RMB_ExchangeRateFeed feed = prvGetMockExchangeRateFeed();
                ICalculator calculator = new Calculator(feed);
                //不同写法

                // .NET 1.x
                Assert.Throws(typeof(DivideByZeroException), new TestDelegate(TC2_DivideByZero));

                // .NET 2.0
                ////Assert.Throws<DivideByZeroException>(TC2_DivideByZero() );
                Assert.Throws<DivideByZeroException>(delegate { calculator.Divide(9, 0); });

                // Using C# 3.0   
                Assert.Throws<DivideByZeroException>(() => { calculator.Divide(9, 0); });

                //对抛出的异常进行核实
                ////DivideByZeroException ex = Assert.Throws<DivideByZeroException>(delegate { throw new DivideByZeroException("message", 42); });
                DivideByZeroException ex = Assert.Throws<DivideByZeroException>(delegate { throw new DivideByZeroException("message" ); });
                Assert.That(ex.Message, Is.EqualTo("message"));
                ////Assert.That(ex.MyParam, Is.EqualTo(42));
            }

            [Test(Description = "Convert 1 USD to RMB. Expected result is 500.")]
            public void TC3_ConvertUSDtoRMBTest()
            {
                IUSD_RMB_ExchangeRateFeed feed = this.prvGetMockExchangeRateFeed();
                ICalculator calculator = new Calculator(feed);
                int actualResult = calculator.ConvertUSDtoRMB(1);
                int expectedResult = 500;
                Assert.AreEqual(expectedResult, actualResult);
            }
        }
    }
}
