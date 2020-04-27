using System;
using System.Collections.Generic;
using System.Text;

namespace CoSMoS.Models
{
    public class Symptoms
    {
        public bool IsFever { get; set; }
        public bool IsCough { get; set; }
        public bool IsRunnyNose { get; set; }
        public bool IsFatigue { get; set; }

        public bool IsShortnessOfBreath { get; set; }
        public bool IsHeadaches { get; set; }
        public bool IsDifficultyInBreathing { get; set; }
        public bool IsDiarrhea { get; set; }
        
        public int Age { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public decimal BodyTemperature { get; set; }

    }
}
