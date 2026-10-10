using AssetManagement.Api.Models;

namespace AssetManagement.Api.Dtos;

public record CreateAssetDto(string Name, string SerialNumber, AssetType Type);