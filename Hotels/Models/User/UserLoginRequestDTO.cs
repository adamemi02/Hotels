
using System.ComponentModel.DataAnnotations;

namespace Hotels.Models.DTOs.Users
{
    public class UserLoginRequestDTO
    {
        [Required]
        public string Username { get; set; }                
        [Required]
        public string Password { get; set; }
    }
}
