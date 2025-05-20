using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace book_library_api.Entities
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("title")]
        public required string Title { get; set; }
        [BsonElement("published")]
        public DateTime Published {get;set;} = DateTime.Now;
        [BsonElement("authorId")]
        public int AuthorId { get; set; }

        [BsonIgnore]
        public Author? Author { get; set; }
       
    }
}
