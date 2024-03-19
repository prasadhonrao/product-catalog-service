using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductAPI.Models;

public class Category
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
}
