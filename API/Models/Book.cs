﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }
        
        [MaxLength(100)]
        public string Category { get; set; }
        
        [MaxLength(255)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string Image {  get; set; }

        [Required, ForeignKey("Author")]
        public int AuthorId { get; set; }

        [MaxLength(100)]
        public string Publisher { get; set; }

        public int Status { get; set; }

        // Navigation properties
        public Author? Author { get; set; }
    }
}
