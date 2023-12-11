using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Author
    {
        [Key, JsonIgnore]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
    }
}
