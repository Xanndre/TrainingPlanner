using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }
        public string Data { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsMiniature { get; set; }
        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}
