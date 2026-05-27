using FlowMind.Core.DbContextOptions;
using FlowMind.Core.DTOs;
using FlowMind.Core.Interfaces;
using FlowMind.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowMind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthRepository authRepo, ITokenService tokenService)
        {
            _authRepo = authRepo;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            if (await _authRepo.UserExists(registerDto.Email))
                return BadRequest(ApiResponse<string>.Failure("Registration failed",new List<string> { "Email is already registered." }));

            var userToCreate = new User
            {
                Email = registerDto.Email.ToLower(),
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PreferredCurrency = registerDto.PreferredCurrency,
            };

            var createdUser = await _authRepo.Register(userToCreate, registerDto.Password);

            var userResponse = new UserResponseDto
            {
                Id = Guid.Parse(createdUser.Id),
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                PreferredCurrency = createdUser.PreferredCurrency,
                Token = _tokenService.CreateToken(createdUser)
            };

            return Ok(ApiResponse<UserResponseDto>.Success(userResponse, "User registered successfully."));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var user = await _authRepo.Login(loginDto.Email, loginDto.Password);

            if (user == null)
                return Unauthorized(ApiResponse<string>.Failure("Authentication failed", new List<string> { "Invalid email or password." }));

            var userResponse = new UserResponseDto
            {
                Id = Guid.Parse(user.Id),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PreferredCurrency = user.PreferredCurrency,
                Token = _tokenService.CreateToken(user)
            };

            return Ok(ApiResponse<UserResponseDto>.Success(userResponse, "Login successful."));        }
    }
}