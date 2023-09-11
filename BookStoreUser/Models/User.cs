using System;
using System.ComponentModel.DataAnnotations;

namespace BookStoreUser.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }

        [MaxLength(100)]
        public required string RegistrationUuid { get; set; }

        [MaxLength(200)]
        public required string FirstName { get; set; }

        [MaxLength(200)]
        public required string LastName { get; set; }

        [MaxLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address")]
        public required string Email { get; set; }

        [DataType(DataType.Currency)]
        public decimal Balance { get; set; } = 0.00M;

        [MaxLength(20)]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number")]
        public required string PhoneNumber { get; set; }

        [MaxLength(250)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [MaxLength(100)]
        public required string Image { get; set; }

        //public override string ToString()
        //{
        //    return $"User(Id: {Id}, FirstName: {FirstName}, LastName: {LastName})";
        //}
    }
}

