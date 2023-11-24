using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public BookController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        // Add a new book
        [HttpPost("AddBook")]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            if (book != null)
            {
                var result = appDbContext.Book.Add(book).Entity;
                await appDbContext.SaveChangesAsync();
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }

        // Get all books
        [HttpGet("GetBooks")]
        public async Task<ActionResult<List<Book>>> GetBooks() => Ok(await appDbContext.Book.ToListAsync());

        // Get a book by Id
        [HttpGet("GetBook/{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await appDbContext.Book.FindAsync(id);
            return book != null ? Ok(book) : NotFound("Book not found");
        }

        // Update book data
        [HttpPut("UpdateBook/{id}")]
        public async Task<ActionResult<Book>> UpdateUser(int id, Book updatedBook)
        {
            var existingBook = await appDbContext.Book.FindAsync(id);

            if (existingBook == null)
            {
                return NotFound("Book not found");
            }

            // Update the properties of the existing book
            existingBook.Title = updatedBook.Title;
            existingBook.Category = updatedBook.Category;
            existingBook.Description = updatedBook.Description;
            existingBook.Image = updatedBook.Image;
            existingBook.AuthorId = updatedBook.AuthorId;
            existingBook.Publisher = updatedBook.Publisher;
            existingBook.Status = updatedBook.Status;


            appDbContext.Book.Update(existingBook);
            await appDbContext.SaveChangesAsync();

            return Ok(existingBook);
        }

        // Delete a book
        [HttpDelete("DeleteBook/{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await appDbContext.Book.FindAsync(id);

            if (book == null)
            {
                return NotFound("Book not found");
            }

            appDbContext.Book.Remove(book);
            await appDbContext.SaveChangesAsync();

            return Ok("Book deleted successfully");
        }

    }
}
