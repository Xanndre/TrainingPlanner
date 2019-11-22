using System;
using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int TrainingId { get; set; }
        public Training Training { get; set; }
        public DateTime Date { get; set; }
        public bool IsReserveList { get; set; }
    }
}
