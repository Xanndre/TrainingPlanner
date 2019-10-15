using System.Collections.Generic;

namespace TrainingPlanner.Core.DTOs
{
    public class ClubCreateDTO
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<ClubPriceBasicDTO> PriceList { get; set; }
    }
}
