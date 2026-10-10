using AssetManagement.Api.Models;

namespace AssetManagement.Api.Dtos;

public record AssetDto(int Id, int? EmployeeId, string Name, string SerialNumber, AssetType Type, AssetStatus Status);