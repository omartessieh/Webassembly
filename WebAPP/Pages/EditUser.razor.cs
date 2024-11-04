using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using WebAPP.Interfaces;
using WebAPP.Models.Dtos;

namespace WebAPP.Pages
{
    public partial class EditUser : ComponentBase
    {
        [Parameter]
        public int userId { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public NavigationManager? NavManager { get; set; }

        private UserDto userDto = new UserDto();


        protected override async Task OnInitializedAsync()
        {
            userDto = await UserService.GetUserById(userId);
        }

        private async Task UserSubmit()
        {
            try
            {
                var result = await UserService.UpdateUser(userDto, userId);

                if (result != null)
                {
                    NavManager.NavigateTo("/user");
                }
                else
                {
                    Console.WriteLine("Failed to update user.");
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