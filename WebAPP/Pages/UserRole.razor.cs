using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using WebAPP.Interfaces;
using WebAPP.Models.Dtos;

namespace WebAPP.Pages
{
    public partial class UserRole : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthStateProvider { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        public IEnumerable<UserRoleViewDto> UsersRole { get; set; } = new List<UserRoleViewDto>();

        private ClaimsPrincipal user;
        private AuthenticationState authState;
        private string searchString = "";

        protected override async Task OnInitializedAsync()
        {
            try
            {
                UsersRole = await UserService.GetUserRole();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving user role: {ex.Message}");
            }

            authState = await AuthStateProvider.GetAuthenticationStateAsync();
            user = authState.User;
        }

        private IEnumerable<UserRoleViewDto> FilteredUsers => string.IsNullOrWhiteSpace(searchString)
        ? UsersRole
        : UsersRole.Where(e => e.Username.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();

        private void UpdateFilteredUsers(ChangeEventArgs e)
        {
            searchString = e.Value?.ToString();
        }

        private async Task AddUserRole()
        {
            await Task.Run(() => NavManager.NavigateTo("/adduserrole"));
        }

        private async Task EditUserRole(int userId)
        {
            await Task.Run(() => NavManager.NavigateTo($"/edituserrole/{userId}"));
        }
    }
}