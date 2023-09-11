using System;
namespace BookStore.Services
{
	public class AppResponse
	{
		public object? Data { get; set; }
		public required string Status { get; set; }
		public string? Message { get; set; } = string.Empty;
	}
}

