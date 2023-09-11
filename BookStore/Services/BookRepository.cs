//using System;
using BookStore.Dtos;
using BookStore.Helpers;
using BookStore.Models;


namespace BookStore.Services
{
	public class BookRepository : IBookRepository
	{
        private StoreDbContext Repository;
        private IWebHostEnvironment HostingEnvironment;

        public BookRepository(StoreDbContext dbContext, IWebHostEnvironment environment)
        {
            Repository = dbContext;
            HostingEnvironment = environment;
        }       

        public async Task<Book> AddBook(AddBookDto bookDto)
        {
            Book? book1 = Repository.Books.FirstOrDefault(bk => bk.Title.ToUpper() == bookDto.Title.ToUpper());

            if (book1 != null)
            {
                throw new AppException("Book already exists");
            }

            //Upload thumbnail
            string thumbnail = Misc.UploadFile(bookDto.Thumbnail, HostingEnvironment);
            Book book = new Book
            {
                Author = bookDto.Author,
                BookUuid = Misc.GenerateReference(),
                CreatedAt = DateTime.Now,
                Thumbnail = thumbnail,
                Title = bookDto.Title,
                Description = bookDto.Description,
                Price = bookDto.Price,
                YearPublished = bookDto.YearPublished
            };
            await Repository.Books.AddAsync(book);
            await Repository.SaveChangesAsync();

            return book;
        }

        public Book GetBook(string bookUuid)
        {
            return Repository.Books.FirstOrDefault(opt => opt.BookUuid == bookUuid)!;
        }

        public Book GetBookById(int id)
        {
            Book? book = Repository.Books.FirstOrDefault(bk => bk.Id == id);
            //if (book == null)
            //{
            //    throw new Exception("Book not exists");
            //}
            return book!;
        }

        public IEnumerable<Book> GetBooks()
        {
            return Repository.Books;
        }
    }
}

