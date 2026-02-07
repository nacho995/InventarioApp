using InventarioApp.Data;
using InventarioApp.Models;
using Microsoft.EntityFrameworkCore;
namespace InventarioApp.Services;

public class CategoriesService
{
    private readonly ApplicationDbContext _db;
    
    public CategoriesService(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _db.Categories.ToListAsync();
    }

    public async Task<string?> CreateOrUpdateCategoryAsync(Category category)
    {
        if (category == null)
            return "Category cannot be null";
        if (string.IsNullOrWhiteSpace(category.Name))
            return "El nombre es obligatorio";
        if (category.Id == 0)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            
            _db.ChangeLogs.Add(new ChangeLog
            {
                Entity = "Category",
                EntityId = category.Id,
                Action = "Create",
                Details = $"Category '{category.Name}' created",
                Date = DateTime.UtcNow
            });
        }else
        {
            _db.Categories.Update(category);
            
            _db.ChangeLogs.Add(new ChangeLog
            {
                Entity = "Category",
                EntityId = category.Id,
                Action = "Update",
                Details = $"Category '{category.Name}' updated",
                Date = DateTime.UtcNow
            });
        }
   
        await _db.SaveChangesAsync();
        return null;
    }

    public async Task<string?> DeleteCategory(int categoryId)
    {
        var category = await _db.Categories.FindAsync(categoryId);
        if (category == null)
        {
            return "Categoría no encontrada";
        }
        
        _db.ChangeLogs.Add(new ChangeLog
        {
            Entity = "Category",
            EntityId = category.Id,
            Action = "Delete",
            Details = $"Category '{category.Name}' deleted",
            Date = DateTime.UtcNow
        });
        _db.Categories.Remove(category);
        await _db.SaveChangesAsync();
        return null;
    }
    
}