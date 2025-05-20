using book_library_api.Entities;
using MongoDB.Driver;

namespace book_library_api.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _booksCollection;

        public BookService(MongoClientConnection mongoClientConnection)
        {
            var client = mongoClientConnection.GetMongoClient();
            var database = client.GetDatabase("book-library");
            _booksCollection = database.GetCollection<Book>("books");
        }

        public async Task<List<Book>> GetBooks() =>
            await _booksCollection.Find(_ => true).ToListAsync();

        public async Task<Book?> GetBookById(string id) =>
            await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Book> CreateBook(Book book)
        {
            await _booksCollection.InsertOneAsync(book);
            return book;
        }

        public async Task<Book> UpdateBook(string id, Book book)
        {
            await _booksCollection.ReplaceOneAsync(x => x.Id == id, book);
            return book;
        }

        public async Task DeleteBook(string id) =>
            await _booksCollection.DeleteOneAsync(x => x.Id == id);
    }
}
