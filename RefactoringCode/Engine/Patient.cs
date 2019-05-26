namespace Engine
{
    public abstract class Patient{

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

        public abstract double DistanceFromIdealWeight();

        public abstract double IdealBodyWeight();

        public abstract double DailyCaloriesRecommended();
    }
}