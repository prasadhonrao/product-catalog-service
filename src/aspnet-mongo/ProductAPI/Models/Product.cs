using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductAPI.Models;

public class Product
{
    [BsonId]
    [BsonIgnore]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ObjectId { get; set; } = string.Empty;

    [BsonElement("guid")]
    public string Guid { get; set; } = string.Empty;
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;
    [BsonElement("quantity")]
    public int Quantity { get; set; }
    [BsonElement("price")]
    public decimal Price { get; set; }
    [BsonElement("status")]
    public Status Status { get; set; }

    [BsonElement("inventoryStatus")]
    public InventoryStatus InventoryStatus { get; set; }
    [BsonElement("categories")]
    public List<Category> Categories { get; set; } = null!;
}
