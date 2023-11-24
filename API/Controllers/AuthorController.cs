using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext appDbContext;

        public AuthorController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpPost("AddAuthor")]
        public async Task<ActionResult<Author>> AddAuthor(Author author)
        {
            if (author != null)
            {
                var result = appDbContext.Author.Add(author).Entity;
                await appDbContext.SaveChangesAsync();
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }

        [HttpGet("GetAuthors")]
        public async Task<ActionResult<List<Author>>> GetAuthors() => Ok(await appDbContext.Author.ToListAsync());

        [HttpGet("GetAuthor/{id}")]
        public async Task<ActionResult<Author>> GetBook(int id)
        {
            var author = await appDbContext.Author.FindAsync(id);
            return author != null ? Ok(author) : NotFound("Author not found");
        }

        [HttpPut("UpdateAuthor/{id}")]
        public async Task<ActionResult<Author>> UpdateAuthor(int id, Author updatedAuthor)
        {
            var existingAuthor = await appDbContext.Author.FindAsync(id);

            if (existingAuthor == null)
            {
                return NotFound("Author not found");
            }

            // Update the properties of the existing author
            existingAuthor.Name = updatedAuthor.Name;          

            appDbContext.Author.Update(existingAuthor);
            await appDbContext.SaveChangesAsync();

            return Ok(existingAuthor);
        }

        [HttpDelete("DeleteAuthor/{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var author = await appDbContext.Author.FindAsync(id);

            if (author == null)
            {
                return NotFound("Author not found");
            }

            appDbContext.Author.Remove(author);
            await appDbContext.SaveChangesAsync();

            return Ok("Author deleted successfully");
        }
    }
}
