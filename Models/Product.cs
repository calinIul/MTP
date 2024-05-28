using System.ComponentModel.DataAnnotations;

namespace ProductManagementApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)] // Maximum length for SQLite TEXT type
        public string Name { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        public int Price { get; set; }

        [Required]
        [MaxLength(1000)] // Maximum length for SQLite TEXT type
        public string Description { get; set; }

        [MaxLength(1000)] // Maximum length for SQLite TEXT type
        public string? ImagePath { get; set; }
    }
}
