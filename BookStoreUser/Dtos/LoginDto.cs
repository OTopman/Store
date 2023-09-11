using System;
using System.ComponentModel.DataAnnotations;

namespace BookStoreUser.Dtos
{
	public class LoginDto
	{
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email address is required")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}

