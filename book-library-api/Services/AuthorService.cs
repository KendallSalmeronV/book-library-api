using book_library_api.Entities;
using MongoDB.Driver;

namespace book_library_api.Services
{
    public class AuthorService
    {
        private readonly IMongoCollection<Author> _authorsCollection;

        public AuthorService(MongoClientConnection mongoClientConnection)
        {
            var client = mongoClientConnection.GetMongoClient();
            var database = client.GetDatabase("book-library");
            _authorsCollection = database.GetCollection<Author>("authors");
        }

        public async Task<List<Author>> GetAuthors() =>
            await _authorsCollection.Find(_ => true).ToListAsync();

        public async Task<Author?> GetAuthorById(string id) =>
            await _authorsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Author> CreateAuthor(Author author)
        {
            await _authorsCollection.InsertOneAsync(author);
            return author;
        }

        public async Task<Author> UpdateAuthor(string id, Author author)
        {
            await _authorsCollection.ReplaceOneAsync(x => x.Id == id, author);
            return author;
        }

        public async Task DeleteAuthor(string id) =>
            await _authorsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
