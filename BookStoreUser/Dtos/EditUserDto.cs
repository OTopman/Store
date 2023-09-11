using System;
using System.ComponentModel.DataAnnotations;

namespace BookStoreUser.Dtos
{
	public class EditUserDto
	{

        [MaxLength(200)]
        [Required(ErrorMessage = "Registration uuid is required")]
        public required string RegistrationUuid { get; set; }

        [MaxLength(200)]
        public string? FirstName { get; set; }

        [MaxLength(200)]
        public string? LastName { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Balance { get; set; }

        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [MaxLength(20)]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number")]
        public string? PhoneNumber { get; set; }

        [MaxLength(100)]
        [DataType(DataType.ImageUrl, ErrorMessage = "Unsupported file type")]
        public IFormFile? Image { get; set; }
    }
}

