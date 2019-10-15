using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class ClubPrice
    {
        [Key]
        public int Id { get; set; }
        public int ClubId { get; set; }
        public Club Club { get; set; }
        public string Name { get; set; }
        public string ValidityPeriod { get; set; }
        public string Entries { get; set; }
        public double Price { get; set; }
    }
}
