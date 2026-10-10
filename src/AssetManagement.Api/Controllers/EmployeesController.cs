using AssetManagement.Api.Services;
using AssetManagement.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<EmployeeDto>>> GetEmployees()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();
        var dtoEmployees = employees.Select(x => x.ToDto()).ToList();
        return Ok(dtoEmployees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> GetById(int id)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        if (employee is null)
        {
            return NotFound();
        }
        return Ok(employee.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> CreateEmployee(CreateEmployeeDto dto)
    {
        var employee = dto.ToEntity();
        var createdEmployee = await _employeeService.CreateEmployeeAsync(employee);
        return CreatedAtAction(nameof(GetById), new { id = createdEmployee.Id }, createdEmployee.ToDto());
    }

    [HttpGet("{id}/assets")]
    public async Task<ActionResult<List<AssetDto>>> GetAssetsOfEmployee(int id)
    {
        var assets = await _employeeService.GetAssetsByEmployeeIdAsync(id);
        if (assets is null)
        {
            return NotFound();
        }
        var dtoAssets = assets.Select(x => x.ToDto()).ToList();
        return Ok(dtoAssets);
    }
}