using book_library_api.Entities;
using book_library_api.Entities.DTO;
using book_library_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace book_library_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorService.GetAuthors();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(string id)
        {
            var author = await _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDTO author)
        {
            if (author == null)
            {
                return BadRequest();
            }
            var createdAuthor = await _authorService.CreateAuthor(new Author { Name  = author.Name});
            return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.Id }, createdAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(string id, [FromBody] AuthorDTO author)
        {

            var updatedAuthor = await _authorService.UpdateAuthor(id, new Author { Name = author.Name, Id = id });
            if (updatedAuthor == null)
            {
                return NotFound();
            }
            return Ok(updatedAuthor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(string id)
        {
            var author = await _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }
            await _authorService.DeleteAuthor(id);
            return NoContent();
        }

    }
}
