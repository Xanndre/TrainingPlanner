using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.ClubStuff;
using TrainingPlanner.Core.DTOs.Stuff;

namespace TrainingPlanner.Core.DTOs.Club
{
    public class ClubUpdateDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<ClubPriceDTO> PriceList { get; set; }
        public ICollection<ClubActivityDTO> Activities { get; set; }
        public ICollection<ClubTrainerDTO> Trainers { get; set; }
        public ICollection<ClubWorkingHoursDTO> WorkingHours { get; set; }
        public ICollection<PictureDTO> Pictures { get; set; }
    }
}
