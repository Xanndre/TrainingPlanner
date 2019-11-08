using System;
using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class ClubCard
    {
        [Key]
        public int Id { get; set; }
        public int ClubId { get; set; }
        public Club Club { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Name { get; set; }
        public string ValidityPeriod { get; set; }
        public string Entries { get; set; }
        public double Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string EntriesLeft { get; set; }
        public string ClubName { get; set; }
    }
}
