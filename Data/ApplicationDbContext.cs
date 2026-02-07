namespace InventarioApp.Data;
using Microsoft.EntityFrameworkCore;
using InventarioApp.Models;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ChangeLog> ChangeLogs { get; set; }
    public DbSet<StockMovement> StockMovements { get; set; }
}