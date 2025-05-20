using book_library_api.Entities;
using book_library_api.Entities.DTO;
using book_library_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace book_library_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(string id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookDTO book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            var createdBook = await _bookService.CreateBook(new Book { Title = book.Title });
            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(string id, [FromBody] BookDTO book)
        {   
            var updatedBook = await _bookService.UpdateBook(id, new Book { Title  = book.Title, Id = id});
            if (updatedBook == null)
            {
                return NotFound();
            }
            return Ok(updatedBook);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            await _bookService.DeleteBook(id);
            return NoContent();
        }
    }
}
