using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NunitTest
{
    [TestFixture]
    public class TestRefactoring
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
    }
}
