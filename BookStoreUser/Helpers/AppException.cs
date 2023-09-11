using System;
namespace BookStoreUser.Helpers
{
	public class AppException : Exception
	{
		private string Status { get; set; } = "failed";
		private int StatusCode { get; set; } = 500;

		public int GetStatusCode()
		{
			return this.StatusCode;
		}

		public string GetStatus()
		{
			return this.Status;
		}

		public AppException(string message, int statusCode): base(message)
		{
			this.StatusCode = statusCode;
		}

		public AppException(string message) : base(message)
		{

		}
	}
}

