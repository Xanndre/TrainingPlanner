namespace TrainingPlanner.Core.DTOs
{
    public class ClubActivityBasicDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int Calories { get; set; }
        public string Level { get; set; }
        public string Picture { get; set; }
        public int ClubId { get; set; }
    }
}
