using System;
namespace BookStoreUser.Services
{
	public class AppResponse<T> : AppResponse
	{
		
		public T? Data { get; set; }
	}

	public class AppResponse
	{
        public required string Status { get; set; }
        public string? Message { get; set; } = string.Empty;
    }
}

