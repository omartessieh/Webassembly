using Microsoft.AspNetCore.Components;
using WebAPP.Interfaces;
using WebAPP.Models.Dtos;

namespace WebAPP.Pages
{
    public partial class EditRole : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public NavigationManager? NavManager { get; set; }

        private RoleDto roleDto = new RoleDto();

        protected override async Task OnInitializedAsync()
        {
            roleDto = await UserService.GetRoleById(Id);
        }

        private async Task RoleSubmit()
        {
            try
            {
                var result = await UserService.UpdateRole(roleDto, Id);

                if (result != null)
                {
                    NavManager.NavigateTo("/role");
                }
                else
                {
                    Console.WriteLine("Failed to update role.");
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