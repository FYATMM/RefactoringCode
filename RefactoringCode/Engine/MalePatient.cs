namespace Engine
{
    public class MalePatient : Patient
    {
        public override  double IdealBodyWeight() => (50+ (2.3 * HeightInInches))* 2.2046;
        public override double DailyCaloriesRecommended() => 66+ (6.3 * WeightInPounds)+ (12.9 * HeightInInches)- (6.8 * Age);

    }
}