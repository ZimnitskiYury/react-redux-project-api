using System.ComponentModel.DataAnnotations;

namespace UserApi.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "The Username must be between 3 and 30 characters long.")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get; set;
        }
    }
}