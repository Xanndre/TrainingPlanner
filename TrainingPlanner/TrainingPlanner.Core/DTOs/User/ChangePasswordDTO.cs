namespace TrainingPlanner.Core.DTOs.User
{
    public class ChangePasswordDTO
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
