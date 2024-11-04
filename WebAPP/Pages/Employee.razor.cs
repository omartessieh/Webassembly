using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Security.Claims;
using WebAPP.Interfaces;
using WebAPP.Models.Dtos;

namespace WebAPP.Pages
{
    public partial class Employee : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthStateProvider { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        public IEnumerable<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();

        private ClaimsPrincipal user;
        private AuthenticationState authState;
        private string searchString = "";


        protected override async Task OnInitializedAsync()
        {
            try
            {
                Employees = await EmployeeService.GetEmployees();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employees: {ex.Message}");
            }

            authState = await AuthStateProvider.GetAuthenticationStateAsync();
            user = authState.User;
        }

        private IEnumerable<EmployeeDto> FilteredEmployees => string.IsNullOrWhiteSpace(searchString)
            ? Employees
            : Employees.Where(e => e.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();

        private void UpdateFilteredEmployees(ChangeEventArgs e)
        {
            searchString = e.Value?.ToString();
        }

        private async Task AddEmployee()
        {
            await Task.Run(() => NavManager.NavigateTo("/addemployee"));
        }

        private async Task EditEmployee(int employeeId)
        {
            await Task.Run(() => NavManager.NavigateTo($"/editemployee/{employeeId}"));
        }

        private async Task DeleteEmployee(int employeeId)
        {
            try
            {
                await EmployeeService.RemoveEmployee(employeeId);
                Employees = await EmployeeService.GetEmployees();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting employee: {ex.Message}");
            }
        }
    }
}