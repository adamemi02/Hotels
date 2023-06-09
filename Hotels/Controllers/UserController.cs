using Hotels.Models.DTOs.Users;
using Hotels.Models;
using Hotels.net.Helpers.Attributes;
using Hotels.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Hotels.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("user")]

        public async Task<ActionResult<UserResponseDTO>> Register(UserRegisterRequestDTO User)
        {
            var user = await _userService.CreateUser(User.FirstName, User.LastName, User.Email, User.Username, User.Password);
            return user is null ? BadRequest("Username is taken!") : Ok(user);
        }

        [HttpPost("admin")]
        [Authorization(Role.Admin)]
        public async Task<ActionResult<UserResponseDTO>> AddAdmin(UserRegisterRequestDTO Admin)
        {
            var admin = await _userService.CreateAdmin(Admin.FirstName, Admin.LastName, Admin.Email, Admin.Username, Admin.Password);
            return admin is null ? BadRequest("Username is taken!") : Ok(admin);
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<UserResponseDTO>> Authenticate(UserLoginRequestDTO User)
        {
            var response = await _userService.Authenticate(User.Username, User.Password);
            return response is null ? BadRequest("Your username/email/password is wrong!") : Ok(response);
        }

        [HttpPost("refreshToken")]
        public async Task<ActionResult<UserResponseDTO>> RefreshToken([FromBody] RefreshTokenRequestDTO request)
        {
            var response = await _userService.RefreshToken(request.Token);
            return response is null ? BadRequest("RefreshToken has expired!") : Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorization(Role.Admin)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUserById(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        [Authorization(Role.Admin)]
        public async Task<ActionResult<UserWithoutTokensResponseDTO>> GetUser(Guid id)
        {
            var user = await _userService.GetUserById(id);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpGet]
        [Authorization(Role.Admin)]
        public async Task<ActionResult<IEnumerable<UserWithoutTokensResponseDTO>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

       
        

    }
}
