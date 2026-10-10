using AssetManagement.Api.Models;
using AssetManagement.Api.Data;
using Microsoft.EntityFrameworkCore; // for async methods

namespace AssetManagement.Api.Services;

public class AssetService : IAssetService
{
    private readonly AppDbContext _context; // "private readonly" equivalent to javas "private final"
    // field is once assigned in the constructor and never changed after that
    public AssetService(AppDbContext context)
    {
        _context = context;
    }

    // "async" in every method to be able to use "await"
    public async Task<Asset> CreateAssetAsync(Asset asset)
    {
        asset.EmployeeId = null; // new assets are not assigned to anybody
        asset.Status = AssetStatus.Available; // new assets are always available
        _context.Assets.Add(asset);
        await _context.SaveChangesAsync();
        return asset;
    }

    public async Task<bool> DeleteAssetAsync(int id)
    {
        var asset = await _context.Assets.FindAsync(id); // get the asset with the given id
        if (asset is null)
        {
            return false;
        }
        _context.Assets.Remove(asset); // marks the asset as "to be deleted"
        await _context.SaveChangesAsync(); // actually deletes the asset
        return true;
    }

    /// <summary>
    /// this method can deliver all assets or (if given) filter by status and only delivers the assets with the given status
    /// </summary>
    public async Task<List<Asset>> GetAllAssetsAsync(AssetStatus? status = null)
    {
        IQueryable<Asset> query = _context.Assets;
        if (status is not null)
        {
            query = query.Where(a => a.Status == status);
        }
        return await query.ToListAsync();
    }

    public async Task<Asset?> GetByIdAsync(int id)
    {
        var asset = await _context.Assets.FindAsync(id);
        return asset;
    }

    public async Task<bool> ModifyAssetAsync(int id, Asset asset)
    {
        var existingAsset = await _context.Assets.FindAsync(id); 
        if (existingAsset is null)
        {
            return false;
        }
        existingAsset.Name = asset.Name;
        existingAsset.SerialNumber = asset.SerialNumber;
        existingAsset.Type = asset.Type;
        await _context.SaveChangesAsync();
        return true;
    }
}