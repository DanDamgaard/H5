using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBookController : ControllerBase
    {
        private readonly AppDbContext appDbContext;

        public UserBookController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        // Rent a book
        [HttpPost("RentBook")]
        public async Task<ActionResult<UserBook>> RentBook(UserBook userBook)
        {
            if (userBook != null)
            {
                // Ensure that the book and user exist
                var bookExists = await appDbContext.Book.AnyAsync(b => b.Id == userBook.BookId);
                var userExists = await appDbContext.User.AnyAsync(u => u.Id == userBook.UserId);

                if (bookExists && userExists)
                {
                    // Check if the user has already rented the same book
                    var existingRental = await appDbContext.UserBook
                        .FirstOrDefaultAsync(ub => ub.BookId == userBook.BookId && ub.UserId == userBook.UserId);

                    if (existingRental != null)
                    {
                        return BadRequest("User has already rented this book");
                    }

                    // Set the EndDate to StartDate + 14 days
                    userBook.EndDate = userBook.StartDate.AddDays(14);

                    var result = appDbContext.UserBook.Add(userBook).Entity;
                    await appDbContext.SaveChangesAsync();
                    return Ok(result);
                }
                return BadRequest("Invalid Book or User");
            }
            return BadRequest("Invalid Request");
        }

        [HttpGet("GetUsersWithRentedBooks")]
        public IActionResult GetUsersWithRentedBooks()
        {
            try
            {
                var result = appDbContext.UserBook
                    .Include(ub => ub.User)
                    .Include(ub => ub.Book)
                    .Select(ub => new
                    {
                        UserName = ub.User.Name,
                        BookId = ub.Book.Id,
                        BookTitle = ub.Book.Title,
                    })
                    .ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately, log if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

    }
}
