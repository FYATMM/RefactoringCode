using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NunitTest
{
    [TestFixture]
    public class TestDriven
    {
        private FemalePaient femalePaient;

        [SetUp()]

        public void CreateFemalePatientInstance()
        {
            femalePaient = new FemalePaient();
            femalePaient.HeightInInches = 72;
            femalePaient.WeightInPounds = 110;
            femalePaient.Age = 30;
        }

        [Test()]
        public void TestIdealBodyWeight()
        {
            double expectedResult = 161.15626;
            double realResult = femalePaient.IdealBodyWeight();

            Assert.AreEqual(expectedResult, realResult);
        }

        [Test()]
        public void TestDailyCaloriesRecommended()
        {
            double expectedResult = 1325.4;
            double realResult = femalePaient.DailyCaloriesRecommended();

            Assert.AreEqual(expectedResult, realResult);
        }

        //先不编写对应的方法，而是先写测试程序
        //然后再写方法让测试通过，通过VS智能生成这个方法的签名
        [Test()]
        public void TestBodyFatContent()
        {
            decimal expectedResult = 36;
            decimal realResult = femalePaient.BodyFatContent();
            Assert.AreEqual(expectedResult, realResult);
        }
    }
}
