using System;
using System.ComponentModel.DataAnnotations;

namespace UserApi.Models
{
    public class RegisterModel
    {
        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "The username must be between 5 and 30 characters long.")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "The FirstName must be between 3 and 30 characters long.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "The FirstName must be between 3 and 30 characters long.")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}