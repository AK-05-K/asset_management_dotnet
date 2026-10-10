using AssetManagement.Api.Services;
using AssetManagement.Api.Dtos;
using AssetManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Api.Controllers;

[ApiController] // tells ASP.NET Core that this is an API Controller

[Route("api/[controller]")] /* base-URL for all endpoints. [controller] is a placeholder for the class name without "controller".
So base-URL for this controller class is "api/assets". */
public class AssetsController : ControllerBase
{
    private readonly IAssetService _assetService;

    public AssetsController(IAssetService assetService)
    {
        _assetService = assetService;
    }

    /// <summary>
    /// ActionResult<>: either the list or a status code
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<AssetDto>>> GetAssets(AssetStatus? status)
    {
        var assets = await _assetService.GetAllAssetsAsync(status);
        var dtoAssets = assets.Select(x => x.ToDto()).ToList();
        return Ok(dtoAssets);
    }

    [HttpGet("{id}")] // id gets appended to the base-URL
    public async Task<ActionResult<AssetDto>> GetById(int id)
    {
        var asset = await _assetService.GetByIdAsync(id);
        if (asset is null)
        {
            return NotFound();
        }
        return Ok(asset.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<AssetDto>> CreateAsset(CreateAssetDto dto)
    {
        var asset = dto.ToEntity();
        var createdAsset = await _assetService.CreateAssetAsync(asset);
        return CreatedAtAction(nameof(GetById), new { id = createdAsset.Id }, createdAsset.ToDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsset(int id, CreateAssetDto dto)
    {
        var asset = dto.ToEntity();
        var updated = await _assetService.ModifyAssetAsync(id, asset);
        if (!updated)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsset(int id)
    {
        var assetDeleted = await _assetService.DeleteAssetAsync(id);
        if (!assetDeleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}