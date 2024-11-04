using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using WebAPP.Interfaces;
using WebAPP.Models.Dtos;
using WebAPP.Services;

namespace WebAPP.Pages
{
    public partial class EditEmployee : ComponentBase
    {
        [Parameter]
        public int employeeId { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public NavigationManager? NavManager { get; set; }

        private EmployeeDto employeeDto = new EmployeeDto();


        protected override async Task OnInitializedAsync()
        {
            employeeDto = await EmployeeService.GetEmployeeById(employeeId);
        }

        private async Task EmployeeSubmit()
        {
            try
            {
                Console.WriteLine($"BirthDate: {employeeDto.BirthDate}");

                var result = await EmployeeService.UpdateEmployee(employeeDto, employeeId);
                Console.WriteLine(result);

                if (result != null)
                {
                    NavManager.NavigateTo("/employee");
                }
                else
                {
                    Console.WriteLine("Failed to update employee.");
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