using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class BodyInjury
    {
        [Key]
        public int Id { get; set; }
        public int BodyMeasurementId { get; set; }
        public BodyMeasurement BodyMeasurement { get; set; }
        public string Injury { get; set; }
    }
}
