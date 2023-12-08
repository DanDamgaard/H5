using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class UserBook
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("User")]
        public int UserId { get; set; }

        [Required, ForeignKey("Book")]
        public int BookId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Display(Name = "IsRented"), JsonIgnore]
        public bool IsRented { get; set; }

        // Navigation properties
        [JsonIgnore] // This will exclude the User property from JSON serialization
        public User? User { get; set; }
        [JsonIgnore] 
        public Book? Book { get; set; }
    }
}
