using WebAPP.Models;
using WebAPP.Models.Dtos;

namespace WebAPP.Interfaces
{
    public interface IUserService
    {
        // Login
        Task<string> LoginUser(LoginRequest loginRequest);

        // Role
        Task<IEnumerable<RoleDto>> GetRoles();
        Task<RoleDto> GetRoleById(int id);
        Task<RoleDto> AddRole(RoleDto role);
        Task<RoleDto> UpdateRole(RoleDto data, int id);

        // AssignRole
        Task<IEnumerable<UserRoleViewDto>> GetUserRole();
        Task<UserRoleViewDto> GetUserRoleById(int id);
        Task<UserRoleDto> AssignRoleToUser(UserRoleDto data);
        Task<UserRoleDto> UpdateUserRole(UserRoleDto data, int id);

        // User
        Task<IEnumerable<UserDto>> GetUsers();
        Task<UserDto> GetUserById(int id);
        Task<UserDto> AddUser(UserDto user);
        Task<UserDto> UpdateUser(UserDto data, int id);
    }
}