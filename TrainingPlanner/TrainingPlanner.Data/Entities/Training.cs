using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Data.Entities
{
    public class Training
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ClubId { get; set; }
        public Club Club { get; set; }
        public int? TrainerId { get; set; }
        public Trainer Trainer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TrainerName { get; set; }
        public string Room { get; set; }
        public string Level { get; set; }
        public int Entries { get; set; }
        public int EntriesLeft { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
