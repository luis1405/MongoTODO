using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoTODO.Core.Entities
{
    public class Document : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; } = DateTime.Now;
    }
}
