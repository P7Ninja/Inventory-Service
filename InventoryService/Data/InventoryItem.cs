namespace InventoryService.Data;

public class InventoryItem
{
    public InventoryItem(int id, int foodId, DateTime expirationDate)
    {
        Id = id;
        FoodId = foodId;
        ExpirationDate = expirationDate;
    }

    public int Id { get; set; }
    public int FoodId { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
