using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoTODO.Core;
using MongoTODO.Core.Entities;

namespace MongoTODO.Repository
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(IOptions<MongoSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var db = client.GetDatabase(options.Value.Database);

            _collection = db.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            var name = documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault();
            if(name != null)
                return ((BsonCollectionAttribute) name).CollectionName;
            else
                return documentType.Name;
        }


        public async Task<IEnumerable<TDocument>> GetAll()
        {
            return await _collection.Find(p => true).ToListAsync();
        }

        public async Task<TDocument> GetById(string Id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, Id);
            return await _collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<TDocument> InsertDocument(TDocument document)
        {
            await _collection.InsertOneAsync(document);
            return document;
        }

        public async Task<TDocument> UpdateDocument(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            return await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public async Task<TDocument> DeleteById(string Id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, Id);
            return await _collection.FindOneAndDeleteAsync(filter);
        }
    }
}
