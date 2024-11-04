using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using WebAPP.Interfaces;
using WebAPP.Models.Dtos;
using WebAPP.Services;

namespace WebAPP.Pages
{
    public partial class User : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthStateProvider { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        public IEnumerable<UserDto> Users { get; set; } = new List<UserDto>();

        private ClaimsPrincipal user;
        private AuthenticationState authState;
        private string searchString = "";

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Users = await UserService.GetUsers();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving roles: {ex.Message}");
            }

            authState = await AuthStateProvider.GetAuthenticationStateAsync();
            user = authState.User;
        }

        private IEnumerable<UserDto> FilteredUsers => string.IsNullOrWhiteSpace(searchString)
        ? Users
        : Users.Where(e => e.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();

        private void UpdateFilteredUsers(ChangeEventArgs e)
        {
            searchString = e.Value?.ToString();
        }

        private async Task AddUser()
        {
            await Task.Run(() => NavManager.NavigateTo("/adduser"));
        }

        private async Task EditUser(int userId)
        {
            await Task.Run(() => NavManager.NavigateTo($"/edituser/{userId}"));
        }
    }
}