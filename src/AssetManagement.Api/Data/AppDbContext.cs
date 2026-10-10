using Microsoft.EntityFrameworkCore;
using AssetManagement.Api.Models;

namespace AssetManagement.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<Employee> Employees => Set<Employee>();
}