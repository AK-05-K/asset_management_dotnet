using AssetManagement.Api.Models;

namespace AssetManagement.Api.Services;

public interface IAssetService
{
    Task<List<Asset>> GetAllAssetsAsync(AssetStatus? status = null); // status as a oarameter, so that filtering is possible
    Task<Asset?> GetByIdAsync(int id); // if no asset is found for the given id, the output can be "null"
    Task<Asset> CreateAssetAsync(Asset asset);
    Task<bool> ModifyAssetAsync(int id, Asset asset);
    Task<bool> DeleteAssetAsync(int id);
}