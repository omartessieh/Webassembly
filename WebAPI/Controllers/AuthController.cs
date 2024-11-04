using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        // Login
        [HttpPost("login")]
        public string Login([FromBody] LoginRequest obj)
        {
            var token = _auth.Login(obj);
            return token;
        }

        // AssignRole
        [HttpGet("GetUserRole")]
        public async Task<IActionResult> GetUserRole()
        {
            var data = await _auth.GetUserRole();
            if (data is null)
            {
                return NotFound("User Role not found.");
            }
            return Ok(data);
        }

        [HttpGet("GetUserRoleById/{id}")]
        public async Task<IActionResult> GetUserRoleById(int id)
        {
            try
            {
                var data = await _auth.GetUserRoleById(id);
                if (data == null)
                {
                    return NotFound("Sorry, but this Role doesn't exist.");
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] UserRole userRole)
        {
            if (userRole == null || userRole.UserId <= 0 || userRole.RoleId <= 0)
            {
                return BadRequest("Invalid user or role ID");
            }

            try
            {
                var result = await _auth.AssignRoleToUser(userRole);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while assigning the role", details = ex.Message });
            }
        }

        [HttpPut("UpdateUserRole/{id}")]
        public async Task<IActionResult> UpdateUserRole(UserRole data, int id)
        {
            var role = await _auth.UpdateUserRole(data, id);
            if (role is null)
            {
                return NotFound("Sorry, but this Role doesn't exist.");
            }
            return Ok(role);
        }

        // Role
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var data = await _auth.GetRoles();
            if (data is null)
            {
                return NotFound("Roles not found.");
            }
            return Ok(data);
        }

        [HttpGet("GetRoleById/{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var data = await _auth.GetRoleById(id);
            if (data is null)
            {
                return NotFound("Sorry, but this role doesn't exist.");
            }
            return Ok(data);
        }

        [HttpPost("addRole")]
        public Role AddRole([FromBody] Role role)
        {
            var addedRole = _auth.AddRole(role);
            return addedRole;
        }

        [HttpPut("UpdateRole/{id}")]
        public async Task<IActionResult> UpdateRole(Role data, int id)
        {
            var role = await _auth.UpdateRole(data, id);
            if (role is null)
            {
                return NotFound("Sorry, but this Role doesn't exist.");
            }
            return Ok(data);
        }

        // User
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var data = await _auth.GetUsers();
            if (data is null)
            {
                return NotFound("Users not found.");
            }
            return Ok(data);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var data = await _auth.GetUserById(id);
            if (data is null)
            {
                return NotFound("Sorry, but this user doesn't exist.");
            }
            return Ok(data);
        }

        [HttpPost("addUser")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            var data = _auth.AddUser(user);
            return Ok(data);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(User user, int id)
        {
            var data = await _auth.UpdateUser(user, id);
            if (data is null)
            {
                return NotFound("Sorry, but this User doesn't exist.");
            }
            return Ok(data);
        }
    }
}