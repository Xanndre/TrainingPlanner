using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class Club
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<ClubWorkingHours> WorkingHours { get; set; }
        public ICollection<ClubRate> Rating { get; set; }
        public ICollection<ClubPrice> PriceList { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public ICollection<ClubActivity> Activities { get; set; }
        public ICollection<ClubTrainer> Trainers { get; set; }
        public ICollection<FavouriteClub> Favourites { get; set; }
        public ICollection<Training> Trainings { get; set; }
        public int ViewCounter { get; set; }
    }
}
