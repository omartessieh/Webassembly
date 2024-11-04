using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Security.Claims;
using WebAPP.Interfaces;
using WebAPP.Models.Dtos;
using static MudBlazor.Defaults.Classes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPP.Pages
{
    public partial class AddUserRole : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public NavigationManager? NavManager { get; set; }

        public IEnumerable<RoleDto> Roles { get; set; } = new List<RoleDto>();

        public IEnumerable<UserDto> Users { get; set; } = new List<UserDto>();

        private UserDto SelectedUser { get; set; }

        private RoleDto SelectedRole { get; set; }
        public string Message { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Users = await UserService.GetUsers();
                Roles = await UserService.GetRoles();
                Message = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving user role: {ex.Message}");
            }
        }

        private async Task AssignRoleToUser()
        {
            if (SelectedUser != null && SelectedRole != null)
            {
                var userRoles = await UserService.GetUserRole();

                var userRoleExists = userRoles.Any(ur => ur.UserId == SelectedUser.Id && ur.RoleId == SelectedRole.Id);

                if (userRoleExists)
                {
                    Snackbar.Add("User already has this role assigned.", Severity.Error);
                    Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomEnd;
                }
                else
                {
                    var data = new UserRoleDto
                    {
                        UserId = SelectedUser.Id,
                        RoleId = SelectedRole.Id
                    };

                    var success = await UserService.AssignRoleToUser(data);

                    if (success != null)
                    {
                        Snackbar.Add("Role assigned successfully.", Severity.Success);
                        Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomEnd;
                        NavManager?.NavigateTo("/userrole");
                    }
                    else
                    {
                        Snackbar.Add("Failed to assign role.", Severity.Error);
                        Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomEnd;
                    }
                }
            }
            else
            {
                Snackbar.Add("Please Select both a User and a Role.", Severity.Error);
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomEnd;
            }
        }

        private void Cancel()
        {
            NavManager.NavigateTo("/userrole");
        }
    }
}