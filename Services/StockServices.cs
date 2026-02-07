using InventarioApp.Data;
using InventarioApp.Models;
using Microsoft.EntityFrameworkCore;
namespace InventarioApp.Services;
public class StockService
{
    private readonly ApplicationDbContext _db;
    public StockService(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<List<Product>> GetProducts()
    {
        return await _db.Products.ToListAsync();
    }
    public async Task<List<StockMovement>> GetStockMovements()
    {
        return await _db.StockMovements
             .Include(m => m.Product)
             .OrderByDescending(m => m.Date)
             .ToListAsync();
    }
    
    public async Task<string?> AddStockMovement(int productId, TipoMovimiento selectedTypes, int quantity, string? notes = null)
    {
        if (productId <= 0 || quantity <= 0) return "Cantidad inválida.";
        var selectedProduct = await _db.Products.FindAsync(productId);
        if (selectedProduct == null) return "Producto no encontrado.";
        switch (selectedTypes)
        {
            case TipoMovimiento.IN:
                selectedProduct.Stock += quantity;
                break;
            case TipoMovimiento.OUT:
                if (selectedProduct.Stock < quantity) return "No hay suficiente stock.";
                selectedProduct.Stock -= quantity;
                break;
            case TipoMovimiento.ADJUST:
                selectedProduct.Stock = quantity;
                break;
        }

        var stockMovement = new StockMovement
        {
            ProductId = selectedProduct.Id,
            Type = selectedTypes,
            Quantity = quantity,
            Notes = notes,
            Date = DateTime.UtcNow
        };
        _db.StockMovements.Add(stockMovement);
        _db.Products.Update(selectedProduct);
        await _db.SaveChangesAsync();
        _db.ChangeLogs.Add(new ChangeLog
        {
            Entity = "StockMovement",
            EntityId = stockMovement.Id,
            Action = "Create",
            Details = $"Movimiento {selectedTypes} de {quantity} para '{selectedProduct.Name}'",
            Date = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();
        return null;
    }
}