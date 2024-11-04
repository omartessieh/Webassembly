using WebAPI.Entities;

namespace WebAPI.Interfaces
{
    public interface IAuthService
    {
        // Login
        string Login(LoginRequest loginRequest);

        // Role
        Task<List<Role>> GetRoles();
        Task<Role> GetRoleById(int id);
        Role AddRole(Role role);
        Task<Role> UpdateRole(Role data, int id);

        // AssignRole
        Task<List<UserRoleView>> GetUserRole();
        Task<UserRoleView> GetUserRoleById(int id);
        Task<UserRole> AssignRoleToUser(UserRole data);
        Task<UserRole> UpdateUserRole(UserRole data, int id);

        // User
        Task<List<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User data, int id);
    }
}