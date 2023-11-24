using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public UserController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        // Add a new user
        [HttpPost("AddUser")]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            if (user != null)
            {
                var result = appDbContext.User.Add(user).Entity;
                await appDbContext.SaveChangesAsync();
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }

        // Get all users
        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<User>>> GetUsers() => Ok(await appDbContext.User.ToListAsync());

        // Get a user by Id
        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await appDbContext.User.FindAsync(id);
            return user != null ? Ok(user) : NotFound("User not found");
        }

        // Update user data
        [HttpPut("UpdateUser/{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User updatedUser)
        {
            var existingUser = await appDbContext.User.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            // Update the properties of the existing user
            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;
            existingUser.Phone = updatedUser.Phone;
            existingUser.Password = updatedUser.Password;
            existingUser.Address = updatedUser.Address;

            appDbContext.User.Update(existingUser);
            await appDbContext.SaveChangesAsync();

            return Ok(existingUser);
        }

        // Delete a user
        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await appDbContext.User.FindAsync(id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            appDbContext.User.Remove(user);
            await appDbContext.SaveChangesAsync();

            return Ok("User deleted successfully");
        }

        // Login
        [HttpGet("Login/{email}/{password}")]
        public async Task<ActionResult<User>> Login(string email, string password)
        {
            if (email is not null && password is not null)
            {
                var user = await appDbContext.User
                    .Where(x => x.Email!.ToLower().Equals(email.ToLower()) && x.Password == password)
                    .FirstOrDefaultAsync();
                return user != null ? Ok(user) : NotFound("User not found");
            }
            return BadRequest("Invalid Request");
        }

        // Check if an email is used
        [HttpGet("CheckEmail/{email}")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            if (email is not null)
            {
                var userWithEmailExists = await appDbContext.User
                    .AnyAsync(x => x.Email!.ToLower() == email.ToLower());

                return Ok(userWithEmailExists);
            }

            return BadRequest("Invalid Request");
        }
    }
}
