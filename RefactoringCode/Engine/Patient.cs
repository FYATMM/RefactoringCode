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

        public double IdealBodyWeightFemale() => (45.5 + (2.3 * HeightInInches)) * 2.2046;
        //{
        //    return (45.5 + (2.3 * HeightInInches)) * 2.2046;
        //}

        public double DailyCaloriesRecommendedFemale() => 655 + (4.3 * WeightInPounds) + (4.7 * HeightInInches) - (4.7 * Age);
        //{
        //    return 655 + (4.3 * WeightInPounds) + (4.7 * HeightInInches) - (4.7 * Age);
        //}

        public double IdealBodyWeightMale() => (50+ (2.3 * HeightInInches))* 2.2046;
        //{
        //    return (50+ (2.3 * HeightInInches))* 2.2046;
        //}

        public double DailyCaloriesRecommendedMale() => 66+ (6.3 * WeightInPounds)+ (12.9 * HeightInInches)- (6.8 * Age);
        //{
        //    return (66+ (6.3 * WeightInPounds)+ (12.9 * HeightInInches)- (6.8 * Age));
        //}

        public double DistanceFromIdealWeight() =>WeightInPounds - (Gender == Gender.Female? IdealBodyWeightFemale() :   IdealBodyWeightMale()) ;
        //{
        //    return (WeightInPounds - (Gender == Gender.Female ? idealBodyWeightFemale() :   IdealBodyWeightMale()) );
        //}
    }
}