using System;
using BookStoreUser.Dtos;
using BookStoreUser.Models;

namespace BookStoreUser.Services
{
	public interface IUserRepository
	{
		User AddUser(AddUserDto user);
		User EditUser(EditUserDto user);
		IEnumerable<User> GetUsers();
		User GetUser(string registrationUuid);
        User GetUserByEmail(string emailAddress);
		User Login(LoginDto loginDto);
    }
}

