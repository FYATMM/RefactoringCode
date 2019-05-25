namespace CaloriesCalculator
{
    public class Patient
    {
        public string ssn { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public double HeightInInches { get; }
        public double Age { get; }
        public double WeightInPounds { get; }

        public Patient()
        {
        }

        private double idealBodyWeightFemale()
        {
            return (45.5 + (2.3 * HeightInInches)) * 2.2046;
        }


        private double DailyCaloriesRecommendedFemale()
        {
            return 655 + (4.3 * WeightInPounds) + (4.7 * HeightInInches) - (4.7 * Age);
        }

        private double IdealBodyWeightMale()
        {
            return (50+ (2.3 * HeightInInches))* 2.2046;
        }

        private double DailyCaloriesRecommendedMale()
        {
            return (66+ (6.3 * WeightInPounds)+ (12.9 * HeightInInches)- (6.8 * Age));
        }
    }
}