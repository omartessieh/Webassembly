using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using WebAPP.Interfaces;
using WebAPP.Models.Dtos;
using WebAPP.Models;

namespace WebAPP.Pages
{
    public partial class Login : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public ILocalStorageService? LocalStorage { get; set; }

        [Inject]
        public AuthenticationStateProvider? AuthStateProvider { get; set; }

        [Inject]
        public NavigationManager? NavManager { get; set; }

        public LoginRequest LoginRequest { get; set; } = new LoginRequest();

        public string LogFaild { get; set; }

        public List<LoginDto> LoginUser { get; set; }

        protected override async Task OnInitializedAsync()
        {
            LogFaild = null;
        }

        public async Task LoginSubmit()
        {
            if (LoginRequest.Username != null && LoginRequest.Password != null)
            {
                var loginRequest = new LoginRequest
                {
                    Username = LoginRequest.Username,
                    Password = LoginRequest.Password
                };

                try
                {
                    var token = await UserService.LoginUser(loginRequest);

                    if (!string.IsNullOrEmpty(token))
                    {
                        await LocalStorage.SetItemAsync("authToken", token);

                        if (AuthStateProvider is CustomAuthStateProvider customAuthStateProvider)
                        {
                            customAuthStateProvider.MarkUserAsAuthenticated(token);
                        }

                        await AuthStateProvider.GetAuthenticationStateAsync();

                        NavManager.NavigateTo("");
                    }
                    else
                    {
                        LogFaild = "Login failed. Please check your credentials.";
                    }
                }
                catch (Exception ex)
                {
                    LogFaild = $"An error occurred: {ex.Message}";
                }
            }
        }
    }
}