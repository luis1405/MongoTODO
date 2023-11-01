using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoTODO.Core.Entities
{
    public interface IDocument 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string? Id { get; set; }

        [BsonElement("createdDate")]
        DateTime CreatedDate { get; }
    }
}
