using BookStore.Application.Services;
using BookStore.Contracts;
using BookStore.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{       
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;
        public BooksController(IBooksService booksService) 
        {
            _booksService = booksService;
        }

        [HttpGet]
        [Authorize(Policy = "User")]
        /*RequireAuthorization(policy => policy.AddRequirement(new PremissionRequirement[Permission.Read]))*/
        public async Task<ActionResult<List<BooksResponse>>> GetBooks()
        {
            var books = await _booksService.GetAllBooks();
            var response = books.Select(b => new BooksResponse(b.Id, b.Title, b.Description, b.Price));
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<Guid>> CreateBook([FromBody] BooksRequest request)
        {
            var (book, error) = Book.Create(
                Guid.NewGuid(),
                request.Title,
                request.Description,
                request.Price);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            var bookId = await _booksService.CreateBook(book);
            return Ok(bookId);
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<Guid>> UpdateBook(Guid id,[FromBody] BooksRequest request)
        {
            var bookId = await _booksService.UpdateBook(id, request.Title, request.Description, request.Price);
            return Ok(bookId);
        }
        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<Guid>> DeleteBook(Guid id)
        {
            var bookId = await _booksService.DeleteBook(id);
            return Ok(bookId);
        }
    }
}
