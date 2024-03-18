namespace ProductCatalogAPI.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Status Status { get; set; }
        public InventoryStatus InventoryStatus { get; set; }
        public List<Category> Categories { get; set; } = null!;
    }
}
