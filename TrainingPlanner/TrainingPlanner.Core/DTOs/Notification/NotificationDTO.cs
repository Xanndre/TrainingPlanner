namespace TrainingPlanner.Core.DTOs.Notification
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool CardAlmostExpired { get; set; }
        public bool CardExpired { get; set; }
        public bool IncomingTraining { get; set; }
        public bool SignUpConfirmed { get; set; }
        public bool SignOutConfirmed { get; set; }
        public bool TrainingDeleted { get; set; }
        public bool ReserveListToList { get; set; }
        public bool ListToReserveList { get; set; }
        public bool ReserveListSignUpConfirmed { get; set; }
        public bool ReserveListSignOutConfirmed { get; set; }
    }
}
