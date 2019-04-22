using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApi.Models
{
    public class TodoItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public string  Id { get; set; }

        [BsonElement("id")]
        public long IdProp { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("isComplete")]
        public bool IsComplete { get; set; } 
    }
}
