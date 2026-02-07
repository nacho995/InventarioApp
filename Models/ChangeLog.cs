namespace InventarioApp.Models;
public class ChangeLog
{
    public int Id { get; set; }
    public string Entity { get; set; } = string.Empty;
    public int EntityId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
}