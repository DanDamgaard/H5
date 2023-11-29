using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
    }
}
