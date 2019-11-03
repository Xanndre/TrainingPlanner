using TrainingPlanner.Core.DTOs.Stuff;
using TrainingPlanner.Core.DTOs.User;

namespace TrainingPlanner.Core.DTOs.ClubStuff.ClubRate
{
    public class ClubRateBaseDTO : RateDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public int ClubId { get; set; }
    }
}
