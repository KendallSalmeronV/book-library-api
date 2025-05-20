using book_library_api.Entities;
using MongoDB.Driver;

namespace book_library_api.Services
{
    public class AuthorService
    {
        private readonly MongoClientConnection _mongoClientConnection;

        public AuthorService()
        {
            _mongoClientConnection = new MongoClientConnection();
        }

        public async Task<List<Author>> GetAuthors()
        {
            var client = _mongoClientConnection.GetMongoClient();
            var database = client.GetDatabase("book-library");
            var collection = database.GetCollection<Author>("authors");
            var authors = await collection.Find(_=>true).ToListAsync();
            return authors;
        }

        public async Task<Author> GetAuthorById(string id)
        {
            var client = _mongoClientConnection.GetMongoClient();
            var database = client.GetDatabase("book-library");
            var collection = database.GetCollection<Author>("authors");
            var author = await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return author;
        }

        public async Task<Author> CreateAuthor(Author author)
        {
            var client = _mongoClientConnection.GetMongoClient();
            var database = client.GetDatabase("book-library");
            var collection = database.GetCollection<Author>("authors");
            await collection.InsertOneAsync(author);
            return author;
        }

        public async Task<Author> UpdateAuthor(string id, Author author)
        {
            var client = _mongoClientConnection.GetMongoClient();
            var database = client.GetDatabase("book-library");
            var collection = database.GetCollection<Author>("authors");
            await collection.ReplaceOneAsync(x => x.Id == id, author);
            return author;
        }

        public async Task DeleteAuthor(string id)
        {
            var client = _mongoClientConnection.GetMongoClient();
            var database = client.GetDatabase("book-library");
            var collection = database.GetCollection<Author>("authors");
            await collection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
