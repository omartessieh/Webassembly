using System.Net.Http.Json;
using WebAPP.Interfaces;
using WebAPP.Models.Dtos;
using WebAPP.Models;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPP.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        // Login
        public async Task<string> LoginUser(LoginRequest loginRequest)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Auth/login", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception("Invalid login attempt");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during the login process: {ex.Message}", ex);
            }
        }

        // Role
        public async Task<IEnumerable<RoleDto>> GetRoles()
        {
            var response = await httpClient.GetAsync("api/Auth/GetRoles");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<RoleDto>>() ?? Enumerable.Empty<RoleDto>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status code: {response.StatusCode}, message: {message}");
        }

        public async Task<RoleDto> GetRoleById(int id)
        {
            var response = await httpClient.GetAsync($"api/Auth/GetRoleById/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RoleDto>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status code: {response.StatusCode}, message: {message}");
        }

        public async Task<RoleDto> AddRole(RoleDto role)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Auth/addRole", role);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<RoleDto>();
                }
                else
                {
                    throw new Exception("Failed to add role");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding the role: {ex.Message}", ex);
            }
        }

        public async Task<RoleDto> UpdateRole(RoleDto data, int id)
        {
            var response = await httpClient.PutAsJsonAsync($"api/Auth/UpdateRole/{id}", data);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RoleDto>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status code: {response.StatusCode}, message: {message}");
        }

        // AssignRole
        public async Task<IEnumerable<UserRoleViewDto>> GetUserRole()
        {
            var response = await httpClient.GetAsync("api/Auth/GetUserRole");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<UserRoleViewDto>>() ?? Enumerable.Empty<UserRoleViewDto>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status code: {response.StatusCode}, message: {message}");
        }

        public async Task<UserRoleViewDto> GetUserRoleById(int id)
        {
            var response = await httpClient.GetAsync($"api/Auth/GetUserRoleById/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserRoleViewDto>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status code: {response.StatusCode}, message: {message}");
        }

        public async Task<UserRoleDto> AssignRoleToUser(UserRoleDto data)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(data),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await httpClient.PostAsync("api/Auth/assignRole", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<UserRoleDto>(jsonResponse);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssignRoleToUser: {ex.Message}");
                return null;
            }
        }

        public async Task<UserRoleDto> UpdateUserRole(UserRoleDto data, int id)
        {
            var response = await httpClient.PutAsJsonAsync($"api/Auth/UpdateUserRole/{id}", data);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserRoleDto>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status code: {response.StatusCode}, message: {message}");
        }

        // User
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var response = await httpClient.GetAsync("api/Auth/GetUsers");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<UserDto>>() ?? Enumerable.Empty<UserDto>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status code: {response.StatusCode}, message: {message}");
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var response = await httpClient.GetAsync($"api/Auth/GetUserById/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDto>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status code: {response.StatusCode}, message: {message}");
        }

        public async Task<UserDto> AddUser(UserDto user)
        {
            var response = await httpClient.PostAsJsonAsync("api/Auth/addUser", user);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDto>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status code: {response.StatusCode}, message: {message}");
        }

        public async Task<UserDto> UpdateUser(UserDto user, int id)
        {
            var response = await httpClient.PutAsJsonAsync($"api/Auth/UpdateUser/{id}", user);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDto>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status code: {response.StatusCode}, message: {message}");
        }
    }
}