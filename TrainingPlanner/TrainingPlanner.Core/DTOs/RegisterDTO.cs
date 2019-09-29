using System;
using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Core.DTOs
{
    public class RegisterDTO : LoginDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string City { get; set; }
    }
}
