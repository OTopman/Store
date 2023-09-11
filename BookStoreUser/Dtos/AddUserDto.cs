using System;
using System.ComponentModel.DataAnnotations;

namespace BookStoreUser.Dtos
{
	public class AddUserDto
	{
        [MaxLength(200)]
        [Required(ErrorMessage = "First name is required")]
        public required string FirstName { get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "Last name is required")]
        public required string LastName { get; set; }

        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email address is required")]
        public required string Email { get; set; }

        [MaxLength(11)]
        [MinLength(11)]
        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number")]
        public required string PhoneNumber { get; set; }

        [MaxLength(250)]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        //[MaxLength(100)]
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.ImageUrl, ErrorMessage = "Unsupported file type")]
        public required IFormFile Image { get; set; }
    }
}

