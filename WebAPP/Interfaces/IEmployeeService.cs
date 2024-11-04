using WebAPP.Models.Dtos;

namespace WebAPP.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployees();
        Task<EmployeeDto> AddEmployee(EmployeeDto employee);
        Task<EmployeeDto> GetEmployeeById(int id);
        Task<EmployeeDto> UpdateEmployee(EmployeeDto data, int id);
        Task<EmployeeDto> RemoveEmployee(int id);
    }
}
