using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Security.Claims;
using WebAPP.Interfaces;
using WebAPP.Models.Dtos;

namespace WebAPP.Pages
{
    public partial class AddEmployee : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public NavigationManager? NavManager { get; set; }

        private EmployeeDto employeeDto = new EmployeeDto();

        private async Task EmployeeSubmit()
        {
            try
            {
                var result = await EmployeeService.AddEmployee(employeeDto);

                if (result != null)
                {
                    NavManager.NavigateTo("/employee");
                }
                else
                {
                    Snackbar.Add("Failed to add employee.", Severity.Error);
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
            NavManager.NavigateTo("/employee");
        }
    }
}