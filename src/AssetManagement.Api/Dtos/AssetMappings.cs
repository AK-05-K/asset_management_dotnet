using AssetManagement.Api.Models;

namespace AssetManagement.Api.Dtos;

public static class AssetMappings
{
    public static AssetDto ToDto(this Asset asset)
    {
        return new AssetDto(asset.Id, asset.EmployeeId, asset.Name, asset.SerialNumber, asset.Type, asset.Status);
    }

    public static Asset ToEntity(this CreateAssetDto dto)
    {
        return new Asset
        {
            Name = dto.Name,
            SerialNumber = dto.SerialNumber,
            Type = dto.Type
        };
    }
}