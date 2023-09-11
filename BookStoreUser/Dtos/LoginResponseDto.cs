using System;
using BookStoreUser.Models;

namespace BookStoreUser.Dtos
{
	public class LoginResponseDto
	{
		public User User { get; set; }
		public AccessToken AccessToken { get; set; }
		
	}

	public class AccessToken
	{
		public string Token { get; set; }
		public DateTime ExpiredAt { get; set; }
	}
}

