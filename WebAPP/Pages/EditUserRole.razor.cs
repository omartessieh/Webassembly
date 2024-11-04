using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Security.Claims;
using WebAPP.Interfaces;
using WebAPP.Models.Dtos;

namespace WebAPP.Pages
{
    public partial class EditUserRole : ComponentBase
    {
        [Parameter]
        public int userId { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public NavigationManager? NavManager { get; set; }

        private UserRoleViewDto userrole = new UserRoleViewDto();

        public IEnumerable<RoleDto> Roles { get; set; } = new List<RoleDto>();

        private RoleDto SelectedRole { get; set; }

        private int id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            userrole = await UserService.GetUserRoleById(userId);
            Roles = await UserService.GetRoles();
        }

        private async Task AssignRoleToUser()
        {
            id = userrole.UserRoleId;

            if (SelectedRole != null)
            {

                var data = new UserRoleDto
                {
                    UserId = userId,
                    RoleId = SelectedRole.Id
                };

                var success = await UserService.UpdateUserRole(data, id);

                if (success != null)
                {
                    NavManager?.NavigateTo("/userrole");
                }
                else
                {
                    Console.WriteLine("Failed to assign role.");
                }
            }
            else
            {
                Snackbar.Add("Please Select a Role.", Severity.Error);
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomEnd;
            }
        }

        private void Cancel()
        {
            NavManager.NavigateTo("/userrole");
        }
    }
}