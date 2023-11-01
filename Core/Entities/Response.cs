namespace MongoTODO.Core.Entities
{
    public class Response<TDocument> where TDocument : IDocument
    {
        public TDocument? Object { get; set; }
        public bool IsSuccess { get; set; } = false;
        public List<string> Errors { get; set; } = new();
    }
}
