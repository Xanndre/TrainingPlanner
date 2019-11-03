using TrainingPlanner.Core.DTOs.Stuff;

namespace TrainingPlanner.Core.DTOs.ClubStuff.ClubRate
{
    public class ClubRateCreateDTO : RateDTO
    {
        public string UserId { get; set; }
        public int ClubId { get; set; }
    }
}
