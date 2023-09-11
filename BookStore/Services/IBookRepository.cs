using System;
using BookStore.Dtos;
using BookStore.Models;
namespace BookStore.Services
{
	public interface IBookRepository
	{
		IEnumerable<Book> GetBooks();
		Book GetBookById(int id);
        Book GetBook(string bookUuid);
        Task<Book> AddBook(AddBookDto book);
	}
}

