namespace Hotels.Models.DTOs.Users
{
    public class UserWithoutTokensResponseDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }      

        public UserWithoutTokensResponseDTO(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;          
            Email = user.Email;
            Username = user.Username;
        }
    }
}
