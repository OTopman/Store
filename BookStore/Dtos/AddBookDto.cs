using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Dtos
{
	public class AddBookDto
	{
       
        [Required(ErrorMessage = "Title is required")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public required string Author { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Invalid price supplied")]
        public decimal Price { get; set; }

        //[MaxLength(4, ErrorMessage = "Invalid year supplied")]
        //[MinLength(4, ErrorMessage = "Invalid year supplied")]
        [Range(maximum: 2023, minimum: 1980, ErrorMessage = "Year published is invalid")]
        [Required(ErrorMessage = "Year published is required.")]
        public required int YearPublished { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.ImageUrl, ErrorMessage = "Unsupported file type")]
        public required IFormFile Thumbnail { get; set; }

    }
}

