using System;

namespace TrainingPlanner.Core.DTOs.Reservation
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TrainingId { get; set; }
        public DateTime Date { get; set; }
        public bool IsReserveList { get; set; }
    }
}
