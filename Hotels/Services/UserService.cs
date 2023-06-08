using Hotels.Helpers.Jwt;
using Hotels.Models;
using Hotels.Models.DTOs.Users;
using Hotels.net.Helpers.Jwt;
using Hotels.Repositories.UserRepository;

namespace Hotels.Services.UserService
{
    public class UserService 
    {
        private readonly UserRepository userRepository;
        private readonly IJwtUtils _jwtUtils;

        public UserService(UserRepository uR, IJwtUtils jwtUtils)
        {
             userRepository=uR ;
            _jwtUtils = jwtUtils;
        }
        public async Task<UserResponseDTO?> CreateUser(string FirstName, string LastName, string Email, string Username, string Password)
        {
            var check_user =userRepository.FindByUsername(Username);

            if (check_user is not null)
                return null;

            var user = new User()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Username = Username,
                Password = Password,
                Role = Role.User
            };
            await userRepository.CreateAsync(user);
            await userRepository.SaveAsync();

            var token = _jwtUtils.GenerateJwtToken(user);
            var refreshToken = _jwtUtils.GenerateRefreshToken(user);

            return new UserResponseDTO(user, token, refreshToken);
        }
        public async Task<UserResponseDTO?> CreateAdmin(string FirstName, string LastName, string Email, string Username, string Password)
        {
            var check_user = userRepository.FindByUsername(Username);
            if (check_user is not null)
                return null;

            var admin = new User()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Username = Username,
                Password =Password,
                Role = Role.Admin
            };
            await userRepository.CreateAsync(admin);
            await userRepository.SaveAsync();

            var token = _jwtUtils.GenerateJwtToken(admin);
            var refreshToken = _jwtUtils.GenerateRefreshToken(admin);

            return new UserResponseDTO(admin, token, refreshToken);
        }
        public async Task<UserResponseDTO?> Authenticate(string Username, string Password)
        {
            var user = userRepository.FindByUsername(Username);
            if(user is null)
                user = userRepository.FindByEmail(Username);

            if(user is null)
                return null;

            var token = _jwtUtils.GenerateJwtToken(user);
            var refreshToken = _jwtUtils.GenerateRefreshToken(user);

            return new UserResponseDTO(user, token, refreshToken);
        }

        public async Task<UserResponseDTO?> RefreshToken(string OldRefreshToken)
        {
            Guid userId = _jwtUtils.ValidateJwtToken(OldRefreshToken);
            if (userId == Guid.Empty)
                return null;

            var user = await userRepository.FindByIdAsync(userId);
            var newToken = _jwtUtils.GenerateJwtToken(user);
            var refreshToken = _jwtUtils.GenerateRefreshToken(user);

            return new UserResponseDTO(user, newToken, refreshToken);

        }
        public async Task DeleteUserById(Guid UserId)
        {
            var user = await userRepository.FindByIdAsync(UserId);
            if(user == null)
                return;
   
            userRepository.Delete(user);
            await userRepository.SaveAsync();
        }

        public async Task<IEnumerable<UserWithoutTokensResponseDTO>> GetAllUsers()
        {
            var users = await userRepository.GetAllAsync();
            return users.ConvertAll(u => new UserWithoutTokensResponseDTO(u));          
        }

        public async Task<UserWithoutTokensResponseDTO> GetUserById(Guid UserId)
        {
            var user = await userRepository.FindByIdAsync(UserId);
            return new UserWithoutTokensResponseDTO(user);
        }
        public UserWithoutTokensResponseDTO GetUserByUsername(string Username)
        {
            var user = userRepository.FindByUsername(Username);
            return new UserWithoutTokensResponseDTO(user);
        }

        
    }
}
