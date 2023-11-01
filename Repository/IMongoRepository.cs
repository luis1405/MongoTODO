using MongoTODO.Core.Entities;

namespace MongoTODO.Repository
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task<IEnumerable<TDocument>> GetAll();

        Task<TDocument> GetById(string Id);

        Task<TDocument> InsertDocument(TDocument document);

        Task<TDocument> UpdateDocument(TDocument document);

        Task<TDocument> DeleteById(string Id);
    }
}
