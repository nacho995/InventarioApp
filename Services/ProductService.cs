using InventarioApp.Models;
using InventarioApp.Data;
namespace InventarioApp.Services;
using Microsoft.EntityFrameworkCore;    

public class ProductService
{
    private readonly ApplicationDbContext _db;
    
    public ProductService(ApplicationDbContext db)
    {
        _db = db;
    } 
    
    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _db.Categories.ToListAsync();
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _db.Products
            .Include(p => p.Category)
            .ToListAsync();
    }
        
    public async Task<string?> CreateOrUpdateProductAsync(Product newProduct)
    {
        if (string.IsNullOrWhiteSpace(newProduct.Name))
            return "El nombre es obligatorio";
        if (newProduct.CategoryId == 0)
            return "Debes seleccionar una categoría";
        if (newProduct.Id == 0)
        {
            _db.Products.Add(newProduct);
            await _db.SaveChangesAsync();
            _db.ChangeLogs.Add(new ChangeLog
            {
                Entity = "Product",
                EntityId = newProduct.Id,
                Action = "Create",
                Details = $"Product '{newProduct.Name}' created",
                Date = DateTime.UtcNow
            });
        }
        else
        {
            _db.Products.Update(newProduct);

            _db.ChangeLogs.Add(new ChangeLog
            {
                Entity = "Product",
                EntityId = newProduct.Id,
                Action = "Update",
                Details = $"Product '{newProduct.Name}' updated",
                Date = DateTime.UtcNow
            });
        }
   
        await _db.SaveChangesAsync();
        return null;
    }
    public async Task<string?> DeleteProduct(int productId)
    {
        var product = await _db.Products.FindAsync(productId);
        if (product == null)
        {
           return "Producto no encontrado";
        }
        
        _db.ChangeLogs.Add(new ChangeLog
        {
            Entity = "Product",
            EntityId = product.Id,
            Action = "Delete",
            Details = $"Product '{product.Name}' deleted",
            Date = DateTime.UtcNow
        });
        _db.Products.Remove(product);
        await _db.SaveChangesAsync();
        return null;
    }
}