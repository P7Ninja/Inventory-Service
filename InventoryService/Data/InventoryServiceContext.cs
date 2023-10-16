using Microsoft.EntityFrameworkCore;

namespace InventoryService.Data;

public class InventoryServiceContext : DbContext
{
    public InventoryServiceContext(DbContextOptions<InventoryServiceContext> options)
        : base(options)
    {
    }

    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<InventoryItem> InventoryItems { get; set; }
}