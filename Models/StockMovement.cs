
namespace InventarioApp.Models;
public enum TipoMovimiento { IN, OUT, ADJUST}
public class StockMovement
{
    
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public TipoMovimiento Type { get; set; } = TipoMovimiento.IN;
    public int Quantity { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
}