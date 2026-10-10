using AssetManagement.Api.Models;

namespace AssetManagement.Api.Dtos;

public static class EmployeeMappings
{
    public static EmployeeDto ToDto(this Employee employee)
    {
        return new EmployeeDto(employee.Id, employee.FirstName, employee.LastName, employee.Email);
    }

    public static Employee ToEntity(this CreateEmployeeDto dto)
    {
        return new Employee
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email
        };
    }
}