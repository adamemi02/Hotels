using Hotels.Models;
using Hotels.Models.Base;
using System.Text.Json.Serialization;

namespace Hotels.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;

        [JsonIgnore]
        public string Password { get; set; } = null!;

        public Role Role { get; set; }      
    }
}
