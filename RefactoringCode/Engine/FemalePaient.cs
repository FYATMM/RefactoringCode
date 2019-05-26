namespace Engine
{
    public class FemalePaient : Patient
    {

        public override double IdealBodyWeight() => (45.5 + (2.3 * (HeightInInches - 60))) * 2.2046;
        public override double DailyCaloriesRecommended() => 655 + (4.3 * WeightInPounds) + (4.7 * HeightInInches) - (4.7 * Age);
        public override double DistanceFromIdealWeight() => WeightInPounds -IdealBodyWeight();
    }
}