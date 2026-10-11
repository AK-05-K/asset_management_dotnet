using AssetManagement.Api.Models;
using AssetManagement.Api.Data;
using Microsoft.EntityFrameworkCore; // for async methods

namespace AssetManagement.Api.Services;

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext _context;

    public EmployeeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        // load employee with all their assigned assets
        var employee = await _context.Employees
            .Include(e => e.Assets)
            .FirstOrDefaultAsync(e => e.Id == id);

        // check if employee exists
        if (employee is null)
        {
            return false;
        }

        // delete employee in assets and set status to "available"
        foreach (var asset in employee.Assets)
        {
            asset.EmployeeId = null;
            asset.Status = AssetStatus.Available;
        }

        // delete employee and save changes
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<List<Asset>?> GetAssetsByEmployeeIdAsync(int id)
    {
        // check if an employee with the given id exists
        bool exists = await _context.Employees.AnyAsync(e => e.Id == id);
        if (!exists)
        {
            return null;
        }
        // returns a list of assets assigned to the employee with the given id
        return await _context.Assets.Where(a => a.EmployeeId == id).ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        return employee;
    }
}

