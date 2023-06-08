using Hotels.Helpers.Jwt;
using Hotels.Repositories;
using Hotels.Repositories.UserRepository;
using Hotels.Services.UserService;

namespace Hotels.Helpers.Middleware
{
    
    public class JwtMiddleware
    {
        private readonly RequestDelegate _nextRequestDelegate;

        public JwtMiddleware(RequestDelegate requestDelegate)
        {
            _nextRequestDelegate = requestDelegate;
        }
        
        public async Task Invoke(HttpContext httpContext,UserRepository userRepository, IJwtUtils jwtUtils)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var userId = jwtUtils.ValidateJwtToken(token);

            if (userId != Guid.Empty)
            {
                httpContext.Items["User"] = await userRepository.FindByIdAsync(userId);
            }

            await _nextRequestDelegate(httpContext);
        }
        
    }
    
}
