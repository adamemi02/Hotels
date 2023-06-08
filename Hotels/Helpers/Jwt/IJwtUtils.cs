using Hotels.Models;

namespace Hotels.Helpers.Jwt
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        public Guid ValidateJwtToken(string token);
        public string GenerateRefreshToken(User user);
    }
}
