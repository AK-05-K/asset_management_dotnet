using AssetManagement.Api.Models;

namespace AssetManagement.Api.Services;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllEmployeesAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task<Employee> CreateEmployeeAsync(Employee employee);
    Task<List<Asset>?> GetAssetsByEmployeeIdAsync(int id); 
}