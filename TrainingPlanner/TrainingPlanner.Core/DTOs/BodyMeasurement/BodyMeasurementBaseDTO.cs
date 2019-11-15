using System;

namespace TrainingPlanner.Core.DTOs.BodyMeasurement
{
    public class BodyMeasurementBaseDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Weight { get; set; }
        public int Fat { get; set; }
        public double Bmi { get; set; }
    }
}
