using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
    }
}
