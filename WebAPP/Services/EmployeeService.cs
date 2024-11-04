using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WebAPP.Interfaces;
using WebAPP.Models.Dtos;

namespace WebAPP.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;

        public EmployeeService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployees()
        {
            await SetAuthorizationHeader();
            var response = await httpClient.GetAsync("api/Employees/GetAll");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<EmployeeDto>>() ?? Enumerable.Empty<EmployeeDto>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status code: {response.StatusCode}, message: {message}");
        }

        public async Task<EmployeeDto> GetEmployeeById(int id)
        {
            await SetAuthorizationHeader();
            var response = await httpClient.GetAsync($"api/Employees/GetEmployeeById/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EmployeeDto>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status code: {response.StatusCode}, message: {message}");
        }

        public async Task<EmployeeDto> AddEmployee(EmployeeDto employee)
        {
            await SetAuthorizationHeader();
            var response = await httpClient.PostAsJsonAsync("api/Employees/AddEmployee", employee);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EmployeeDto>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status code: {response.StatusCode}, message: {message}");
        }

        public async Task<EmployeeDto> UpdateEmployee(EmployeeDto employee, int id)
        {
            await SetAuthorizationHeader();
            var response = await httpClient.PutAsJsonAsync($"api/Employees/UpdateEmployee/{id}", employee);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EmployeeDto>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status code: {response.StatusCode}, message: {message}");
        }

        public async Task<EmployeeDto> RemoveEmployee(int id)
        {
            await SetAuthorizationHeader();
            var response = await httpClient.DeleteAsync($"api/Employees/RemoveEmployee/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EmployeeDto>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status code: {response.StatusCode}, message: {message}");
        }

        private async Task SetAuthorizationHeader()
        {
            var token = await localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("No authentication token found");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}