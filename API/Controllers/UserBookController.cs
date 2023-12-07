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
                        .FirstOrDefaultAsync(ub => ub.BookId == userBook.BookId && ub.UserId == userBook.UserId && ub.IsRented);

                    if (existingRental != null)
                    {
                        return BadRequest("User has already rented this book");
                    }

                    // Set the EndDate to StartDate + 14 days
                    userBook.EndDate = userBook.StartDate.AddDays(14);

                    // Set IsRented to true
                    userBook.IsRented = true;

                    var result = appDbContext.UserBook.Add(userBook).Entity;
                    await appDbContext.SaveChangesAsync();
                    return Ok(result);
                }
                return BadRequest("Invalid Book or User");
            }
            return BadRequest("Invalid Request");
        }

        // Get all rented books
        [HttpGet("GetRentedBooks")]
        public async Task<ActionResult<IEnumerable<UserBook>>> GetRentedBooks()
        {
            try
            {
                var rentedBooks = await appDbContext.UserBook
                    .Include(ub => ub.Book) 
                    .Include(ub => ub.User)
                    .Select(ub => new
                    {
                        Id= ub.Id,
                        BookId = ub.BookId,
                        UserId = ub.UserId,
                        BookTitle = ub.Book.Title,
                        UserName = ub.User.Name,
                        StartDate = ub.StartDate,
                        EndDate = ub.EndDate,
                        IsRented = ub.IsRented
                    }).ToListAsync();

                return Ok(rentedBooks);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Re-rent a book
        [HttpPost("ReRentBook")]
        public async Task<ActionResult<UserBook>> ReRentBook(UserBook userBook)
        {
            if (userBook != null)
            {
                // Ensure that the book and user exist
                var bookExists = await appDbContext.Book.AnyAsync(b => b.Id == userBook.BookId);
                var userExists = await appDbContext.User.AnyAsync(u => u.Id == userBook.UserId);

                if (bookExists && userExists)
                {
                    // Check if the user has rented the book
                    var existingRental = await appDbContext.UserBook
                        .FirstOrDefaultAsync(ub => ub.BookId == userBook.BookId && ub.UserId == userBook.UserId);

                    if (existingRental == null)
                    {
                        return BadRequest("User has not rented this book");
                    }

                    // Update StartDate and EndDate
                    existingRental.StartDate = userBook.StartDate;
                    existingRental.EndDate = userBook.StartDate.AddDays(14);

                    // Set IsRented to true
                    existingRental.IsRented = true;

                    // Save changes to the database
                    appDbContext.UserBook.Update(existingRental);
                    await appDbContext.SaveChangesAsync();

                    return Ok(existingRental);
                }
                return BadRequest("Invalid Book or User");
            }
            return BadRequest("Invalid Request");
        }

        // Return a rented book
        [HttpPost("ReturnBook")]
        public async Task<ActionResult<UserBook>> ReturnBook(UserBook userBook)
        {
            if (userBook != null)
            {
                // Ensure that the book and user exist
                var bookExists = await appDbContext.Book.AnyAsync(b => b.Id == userBook.BookId);
                var userExists = await appDbContext.User.AnyAsync(u => u.Id == userBook.UserId);

                if (bookExists && userExists)
                {
                    // Check if the user has rented the book
                    var existingRental = await appDbContext.UserBook
                        .FirstOrDefaultAsync(ub => ub.BookId == userBook.BookId && ub.UserId == userBook.UserId);

                    if (existingRental == null)
                    {
                        return BadRequest("User has not rented this book");
                    }

                    // Set IsRented to false
                    existingRental.IsRented = false;

                    // Update the EndDate
                    existingRental.EndDate = DateTime.UtcNow; 

                    appDbContext.Entry(existingRental).State = EntityState.Modified;

                    // Save the changes to the database
                    await appDbContext.SaveChangesAsync();

                    return Ok(existingRental);
                }
                return BadRequest("Invalid Book or User");
            }
            return BadRequest("Invalid Request");
        }

        [HttpGet("GetUsersWithRentedBooks")]
        public async Task<ActionResult<List<UserBook>>> GetUsersWithRentedBooks()
        {        
                var UserBooks = await appDbContext.UserBook
                    .Include(ub => ub.User)
                    .Include(ub => ub.Book)
                    .Select(ub => new
                    {
                        UserName = ub.User.Name,
                        BookId = ub.Book.Id,
                        BookTitle = ub.Book.Title,
                    }).ToListAsync();

                return Ok(UserBooks);          
        }
    }
}
