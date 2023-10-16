namespace InventoryService.Data;

public class Inventory
{
    public Inventory(int userID, string name)
    {
        Id = userID;
        Items = new List<InventoryItem>();
        Name = name;
    }

    public Inventory()
    {
        Id = 0;
        UserId = 0;
        Name = "Inventory";
        Items = new List<InventoryItem>();
    }


    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public List<InventoryItem> Items { get; set; }
}
