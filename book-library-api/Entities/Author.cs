using MongoDB.Bson.Serialization.Attributes;

namespace book_library_api.Entities
{
    public class Author
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public required string Name { get; set; }
    }
}
