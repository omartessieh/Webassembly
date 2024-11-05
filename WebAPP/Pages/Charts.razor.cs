using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using WebAPP.Interfaces;
using WebAPP.Models.Dtos;
using WebAPP.Services;

namespace WebAPP.Pages
{
    public partial class Charts : ComponentBase
    {
        [Inject]
        public IJSRuntime JS { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthStateProvider { get; set; }

        public IEnumerable<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();

        private ClaimsPrincipal user;
        private AuthenticationState authState;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Employees = await EmployeeService.GetEmployees();

                await JS.InvokeVoidAsync("renderChartbar", Employees.Select(s => s.Name).ToArray(), Employees.Select(s => s.Salary).ToArray());
                await JS.InvokeVoidAsync("renderChartpie", Employees.Select(s => s.Name).ToArray(), Employees.Select(s => s.Salary).ToArray());
                await JS.InvokeVoidAsync("renderChartline", Employees.Select(s => s.Name).ToArray(), Employees.Select(s => s.Salary).ToArray());
                await JS.InvokeVoidAsync("renderChartpolararea", Employees.Select(s => s.Name).ToArray(), Employees.Select(s => s.Salary).ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employees: {ex.Message}");
            }

            authState = await AuthStateProvider.GetAuthenticationStateAsync();
            user = authState.User;
        }
    }
}