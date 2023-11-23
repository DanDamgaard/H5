using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string? Name { get; set; }

        [Required, MaxLength(100)]
        public string? Email { get; set; }

        [Required, MaxLength(100)]
        public string? Password { get; set; }

        [MaxLength(100)]
        public string? Phone { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        public int Roles { get; set; }
        
    }
}
