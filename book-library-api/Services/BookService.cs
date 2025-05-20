using book_library_api.Entities;
using MongoDB.Driver;

namespace book_library_api.Services
{
    public class BookService
    {
        private readonly MongoClientConnection _mongoClientConnection;

        public BookService()
        {
            _mongoClientConnection = new MongoClientConnection();
        }

        public async Task<List<Book>> GetBooks()
        {
            var client = _mongoClientConnection.GetMongoClient();
            var database = client.GetDatabase("book-library");
            var collection = database.GetCollection<Book>("books");
            var books = await collection.Find(_ => true).ToListAsync();
            return books;
        }

        public async Task<Book> GetBookById(string id)
        {
            var client = _mongoClientConnection.GetMongoClient();
            var database = client.GetDatabase("book-library");
            var collection = database.GetCollection<Book>("books");
            var book = await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return book;
        }

        public async Task<Book> CreateBook(Book book)
        {
            var client = _mongoClientConnection.GetMongoClient();
            var database = client.GetDatabase("book-library");
            var collection = database.GetCollection<Book>("books");
            await collection.InsertOneAsync(book);
            return book;
        }

        public async Task<Book> UpdateBook(string id, Book book)
        {
            var client = _mongoClientConnection.GetMongoClient();
            var database = client.GetDatabase("book-library");
            var collection = database.GetCollection<Book>("books");
            await collection.ReplaceOneAsync(x => x.Id == id, book);
            return book;
        }

        public async Task DeleteBook(string id)
        {
            var client = _mongoClientConnection.GetMongoClient();
            var database = client.GetDatabase("book-library");
            var collection = database.GetCollection<Book>("books");
            await collection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
