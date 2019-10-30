namespace TrainingPlanner.Core.DTOs.Account
{
    public class ExternalLoginDTO
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
    }
}
