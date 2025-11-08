using AutoMapper;
using InventoryApi.DTOs;
using InventoryApi.Entities;
using InventoryApi.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InventoryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        public AuthController(IAuthService auth, IUserRepository userRepo, IMapper mapper)
        {
            _auth = auth;
            _userRepo = userRepo;
            _mapper = mapper;
        }


        /// <summary>
        /// Get all User
        /// </summary>
        [HttpGet("all-user")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsersAsync();
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(users);
        }


        /// <summary>
        /// Register a new user
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto dto)
        {
            var existingUser = await _userRepo.GetByUsernameAsync(dto.Username!);

            if (existingUser != null)
            {
                return BadRequest(new { error = "Username already exists" });
            }

            var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.Username!,
                Password = hash,
                Role = dto.Role,
            };
            await _userRepo.CreateAsync(user);
            return Ok(new { message = "User registered successfully" });
        }


        /// <summary>
        /// login
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            // For the coding test keep it simple: validate against in-memory credentials or seeded users
            //if (dto.Username == "admin" && dto.Password == "admin123")
            //{
            //    var token = _auth.GenerateJwt(dto.Username, "Admin");
            //    return Ok(new { token });
            //}
            //if (dto.Username == "customer" && dto.Password == "cust123")
            //{
            //    var token = _auth.GenerateJwt(dto.Username, "Customer");
            //    return Ok(new { token });
            //}
            //return Unauthorized();

            var user = await _userRepo.GetByUsernameAsync(dto.Username!);

            if (user == null && !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password)) 
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var token = _auth.GenerateJwt(user.Username!, user.Role!);
            return Ok(new { token } );
        }
    }
}
