using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Models
{
	public class Order
	{
		[Key]
		public int Id { get; set; }

        [MaxLength(100)]
        public required string OrderUuid { get; set; }

        [MaxLength(100)]
        public required string BookUuid { get; set; }

		[MaxLength(100)]
        public required string RegistrationUuid { get; set; }
		public decimal Amount { get; set; }
		public required string Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

	}
}

