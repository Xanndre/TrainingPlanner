using System.ComponentModel.DataAnnotations;
using TrainingPlanner.Core.Utils;

namespace TrainingPlanner.Core.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = DictionaryResources.PasswordLength, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
