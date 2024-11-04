using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Data;
using WebAPI.Entities;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Login
        public string Login(LoginRequest loginRequest)
        {
            if (loginRequest.Username != null && loginRequest.Password != null)
            {
                var user = _context.Users.SingleOrDefault(s => s.Username == loginRequest.Username && s.Password == loginRequest.Password);
                if (user != null)
                {
                    var claims = new List<Claim> {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("UserName", user.Name)
                    };
                    var userRoles = _context.UserRoles.Where(u => u.UserId == user.Id).ToList();
                    var roleIds = userRoles.Select(s => s.RoleId).ToList();
                    var roles = _context.Roles.Where(r => roleIds.Contains(r.Id)).ToList();
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwtToken;
                }
                else
                {
                    throw new Exception("user is not valid");
                }
            }
            else
            {
                throw new Exception("credentials are not valid");
            }
        }

        // Role
        public async Task<List<Role>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return roles;
        }

        public async Task<Role> GetRoleById(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            return role;
        }

        public Role AddRole(Role role)
        {
            var addedRole = _context.Roles.Add(role);
            _context.SaveChanges();
            return addedRole.Entity;
        }

        public async Task<Role> UpdateRole(Role data, int id)
        {
            var Role = await _context.Roles.FindAsync(id);
            if (Role != null)
            {
                Role.Name = data.Name;
                Role.Description = data.Description;
                await _context.SaveChangesAsync();
            }
            return Role;
        }

        // AssignRole
        public async Task<List<UserRoleView>> GetUserRole()
        {
            var userrole = await _context.UserRoleViews.ToListAsync();
            return userrole;
        }

        public async Task<UserRoleView> GetUserRoleById(int id)
        {
            var userrole = await _context.UserRoleViews.FirstOrDefaultAsync(x => x.UserId == id);
            return userrole;
        }

        public async Task<UserRole> AssignRoleToUser(UserRole data)
        {
            var user = await _context.Users.FindAsync(data.UserId);
            var role = await _context.Roles.FindAsync(data.RoleId);

            if (user == null || role == null)
            {
                throw new InvalidOperationException("User or Role not found");
            }

            var existingUserRole = await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == data.UserId && ur.RoleId == data.RoleId);

            if (existingUserRole != null)
            {
                throw new InvalidOperationException("Role is already assigned to the user");
            }

            _context.UserRoles.Add(data);
            await _context.SaveChangesAsync();

            return data;
        }

        public async Task<UserRole> UpdateUserRole(UserRole data, int id)
        {
            var role = await _context.UserRoles.FindAsync(id);
            if (role != null)
            {
                role.UserId = data.UserId;
                role.RoleId = data.RoleId;
                await _context.SaveChangesAsync();
            }
            return role;
        }

        // User
        public async Task<List<User>> GetUsers()
        {
            var Users = await _context.Users.ToListAsync();
            return Users;
        }

        public async Task<User> GetUserById(int id)
        {
            var User = await _context.Users.FindAsync(id);
            return User;
        }

        public async Task<User> AddUser(User user)
        {
            var addedUser = await _context.Users.AddAsync(user);
            _context.SaveChanges();
            return addedUser.Entity;
        }

        public async Task<User> UpdateUser(User data, int id)
        {
            var User = await _context.Users.FindAsync(id);
            if (User != null)
            {
                User.Name = data.Name;
                User.Username = data.Username;
                User.Password = data.Password;
                await _context.SaveChangesAsync();
            }
            return User;
        }
    }
}