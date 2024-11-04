using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Security.Claims;
using WebAPP.Interfaces;
using WebAPP.Models.Dtos;

namespace WebAPP.Pages
{
    public partial class AddUser : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public NavigationManager? NavManager { get; set; }

        private UserDto userDto = new UserDto();

        private async Task UserSubmit()
        {
            try
            {
                var result = await UserService.AddUser(userDto);
                Console.WriteLine(result);

                if (result != null)
                {
                    NavManager.NavigateTo("/user");
                }
                else
                {
                    Snackbar.Add("Failed to add user.", Severity.Error);
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
            NavManager.NavigateTo("/user");
        }
    }
}
