namespace TrainingPlanner.Core.DTOs.ClubStuff
{
    public class ClubWorkingHoursDTO
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public string OpenHour { get; set; }
        public string CloseHour { get; set; }
        public int ClubId { get; set; }
    }
}
