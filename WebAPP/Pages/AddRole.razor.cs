using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Security.Claims;
using WebAPP.Interfaces;
using WebAPP.Models.Dtos;

namespace WebAPP.Pages
{
    public partial class AddRole : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public NavigationManager? NavManager { get; set; }

        private RoleDto roleDto = new RoleDto();

        private async Task RoleSubmit()
        {
            try
            {
                var result = await UserService.AddRole(roleDto);

                if (result != null)
                {
                    NavManager.NavigateTo("/role");
                }
                else
                {
                    Snackbar.Add("Failed to add role.", Severity.Error);
                    Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomEnd;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void Cancel()
        {
            NavManager.NavigateTo("/role");
        }
    }
}