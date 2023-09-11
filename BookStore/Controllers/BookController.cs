using BookStore.Dtos;
using BookStore.Helpers;
using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class BookController : Controller
    {
        private readonly IBookRepository BookRepository;
        public BookController(IBookRepository bookRepository)
        {
            BookRepository = bookRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var books = BookRepository.GetBooks();

            if (books.Count() <= 0)
            {
                var response = new AppResponse
                {
                    Status = "failed",
                    Message = "No record found."
                };
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult(response);
            }
            else
            {
                var res = new AppResponse
                {
                    Status = "success",
                    Data = books,
                    Message = "Books retrieved successfully."
                };

                Response.StatusCode = StatusCodes.Status200OK;
                return new JsonResult(res);
            }

        }

        // GET api/values/5
        [HttpGet("{bookUuid}")]
        public IActionResult Get(string bookUuid)
        {
            Book book = BookRepository.GetBook(bookUuid);
            if (book is null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult(new AppResponse
                {
                    Status = "failed",
                    Message = "Book not found"
                });
            }
            else
            {
                Response.StatusCode = StatusCodes.Status200OK;
                return Json(new AppResponse
                {
                    Status = "success",
                    Message = "Record retrieved successfully.",
                    Data = book
                });
            }

        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AddBookDto bookDto)
        {
            try
            {
                Book book = await BookRepository.AddBook(bookDto);
                Response.StatusCode = StatusCodes.Status201Created;
                return new JsonResult(new AppResponse { Status = "success", Data = book, Message = "Book added successfully." });
            }
            catch (AppException ex)
            {
                Response.StatusCode = ex.GetStatusCode();
                return new JsonResult(new AppResponse { Status = ex.GetStatus() ?? "failed", Message = ex.Message });
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return new JsonResult(new AppResponse { Status = "failed", Message = "Unknown error occured" });
            }
        }

    }
}

