using System;

namespace NunitTest
{
    public abstract class Patient{

        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public double HeightInInches
        {
            get { return HeightInInches; }
            set
            {
                if (value <= 60)
                {
                    throw  new  ArgumentOutOfRangeException("Height has to be greater than five feet(60 inches)");
                }
            }
        }
        public double Age { get; set; }
        public double WeightInPounds { get; set; }
        public Patient()
        {

        }
        public  double DistanceFromIdealWeight() => WeightInPounds - IdealBodyWeight();
        //将条件逻辑移到patient类内，因为这个条件已经是patient的成员了，减少暴露的类，简化使用

        public abstract double IdealBodyWeight();
        public abstract double DailyCaloriesRecommended();               
        
    }
}