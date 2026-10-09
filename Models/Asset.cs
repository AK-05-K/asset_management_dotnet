namespace AssetManagement.Api.Models;

public class Asset
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // nullable is enabled in AssetManagement.Api.csproj,
                                                     // so strings must not be null (just create an empty string)
    public string SerialNumber { get; set; } = string.Empty; 
    public AssetType Type { get; set; }
    public AssetStatus Status { get; set; }
    public int? EmployeeId { get; set; } // "?" to make it nullable, if this asset belongs to no employee
}