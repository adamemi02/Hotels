namespace Hotels.Models.DTOs.Users
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public UserResponseDTO(User user, string token, string refreshToken)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Token = token;
            RefreshToken = refreshToken;
            Email = user.Email;
            Username = user.Username;
        }
    }
}
