namespace MongoTODO.Core.Entities
{
    [BsonCollection("Task")]
    public class TodoTask : Document
    {
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}
