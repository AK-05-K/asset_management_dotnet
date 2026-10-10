namespace AssetManagement.Api.Models;

// four defined types for assets
// Enums give the first referenced value when an asset is defined. So it automatically gets "Other"
public enum AssetType
{
    Other,
    Laptop,
    Monitor,
    Smartphone
}