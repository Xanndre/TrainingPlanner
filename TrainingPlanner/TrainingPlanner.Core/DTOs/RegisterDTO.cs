using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Core.DTOs
{
    public class RegisterDTO : LoginDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
