namespace Engine
{
    public class Patient{

        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public double HeightInInches { get; set; }
        public double Age { get; set; }
        public double WeightInPounds { get; set; }
        public Gender Gender { get; set; }
        public Patient()
        {
        }

        //将条件逻辑移到patient类内，因为这个条件已经是patient的成员了，减少暴露的类，简化使用
        public  double DailyCaloriesRecommended() => Gender == Gender.Male ? DailyCaloriesRecommendedMale() : DailyCaloriesRecommendedFemale();
        public  double IdealBodyWeight() => Gender == Gender.Male ? IdealBodyWeightMale() : IdealBodyWeightFemale();

        private double IdealBodyWeightFemale() => (45.5 + (2.3 * HeightInInches)) * 2.2046;

        private double DailyCaloriesRecommendedFemale() => 655 + (4.3 * WeightInPounds) + (4.7 * HeightInInches) - (4.7 * Age);

        private double IdealBodyWeightMale() => (50+ (2.3 * HeightInInches))* 2.2046;

        private double DailyCaloriesRecommendedMale() => 66+ (6.3 * WeightInPounds)+ (12.9 * HeightInInches)- (6.8 * Age);

        public double DistanceFromIdealWeight() =>WeightInPounds - (Gender == Gender.Female? IdealBodyWeightFemale() :   IdealBodyWeightMale()) ;

    }
}