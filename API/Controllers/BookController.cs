using API.Data;
using API.Models;
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
                // Check if the author is exist
                var existingAuthor = appDbContext.Author.FirstOrDefault(a => a.Name == book.Author.Name);

                if (existingAuthor != null)
                {
                    // Author exists, assign the existing author to the book
                    book.Author = existingAuthor;
                }
                else
                {
                    // Author doesn't exist, create a new author
                    Author newAuthor = new Author { Name = book.Author?.Name }; 
                    appDbContext.Author.Add(newAuthor);
                    await appDbContext.SaveChangesAsync();
                    // Assign the new author to the book
                    book.Author = newAuthor;
                }
                var result = appDbContext.Book.Add(book).Entity;
                await appDbContext.SaveChangesAsync();
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }

        // Get all books
        [HttpGet("GetBooks")]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var books = await appDbContext.Book
                .Include(b => b.Author)
                .Select(b => new
                {
                    Id=b.Id,
                    Title= b.Title,
                    Category = b.Category,
                    Description = b.Description,
                    Image = b.Image,
                    AuthorName = b.Author.Name,
                    Publisher = b.Publisher,
                    Status = b.Status
                }).ToListAsync();
         
            return Ok(books);
        }

        // Get a book by Id
        [HttpGet("GetBook/{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await appDbContext.Book
                .Include(b => b.Author)  // Include the Author data
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound("Book not found");
            }

            var bookWithAuthorName = new
            {
                Id = book.Id,
                Title = book.Title,
                Category = book.Category,
                Description = book.Description,
                Image = book.Image,
                AuthorName = book.Author?.Name,  // Include Author's name
                Publisher = book.Publisher,
                Status = book.Status
            };

            return Ok(bookWithAuthorName);
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
            // Check if the author is exists
            var existingAuthor = appDbContext.Author.FirstOrDefault(a => a.Name == updatedBook.Author.Name);

            if (existingAuthor != null)
            {
                // Author exists, assign the existing author to the book
                existingBook.Author = existingAuthor;
            }
            else
            {
                // Author doesn't exist, create a new author
                Author newAuthor = new Author { Name = updatedBook.Author?.Name };
                appDbContext.Author.Add(newAuthor);
                await appDbContext.SaveChangesAsync();

                // Assign the new author to the book
                existingBook.Author = newAuthor;
            }
            // Update the properties of the existing book
            existingBook.Title = updatedBook.Title;
            existingBook.Category = updatedBook.Category;
            existingBook.Description = updatedBook.Description;
            existingBook.Image = updatedBook.Image;
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
