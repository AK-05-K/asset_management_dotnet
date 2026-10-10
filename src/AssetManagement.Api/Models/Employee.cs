namespace AssetManagement.Api.Models;

// required is used in the string fields, because an employee must have a name and an email
public class Employee
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }

    public List<Asset> Assets { get; set; } = new List<Asset>();

}