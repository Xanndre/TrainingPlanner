using System.Collections.Generic;

namespace TrainingPlanner.Core.DTOs.BodyMeasurement
{
    public class BodyMeasurementDTO : BodyMeasurementBaseDTO
    {
        public int Age { get; set; }
        public int MuscleMass { get; set; }
        public int Height { get; set; }
        public int MetabolicAge { get; set; }
        public int FatMass { get; set; }
        public bool IsInjured { get; set; }
        public int? Neck { get; set; }
        public int? Forearm { get; set; }
        public int? Chest { get; set; }
        public int? Waist { get; set; }
        public int? Thigh { get; set; }
        public int? Shoulders { get; set; }
        public int? Biceps { get; set; }
        public int? Hips { get; set; }
        public int? Calf { get; set; }
        public ICollection<BodyInjuryDTO> Injuries { get; set; }
    }
}
