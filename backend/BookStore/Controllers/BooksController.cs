using BookStore.Application.Services;
using BookStore.Contracts;
using BookStore.Core.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookStore.Controllers
{       
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;
        private readonly IValidator<BooksRequest> _validator;
        public BooksController(IBooksService booksService, IValidator<BooksRequest> validator) 
        {
            _booksService = booksService;
            _validator = validator;
        }

        [HttpGet]
        [Authorize(Policy = "User")]
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
            ValidationResult results = _validator.Validate(request);

            if (!results.IsValid)
            {
                return BadRequest(results.Errors);
            }

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
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<Guid>> UpdateBook(Guid id,[FromBody] BooksRequest request)
        {
            if (!await _booksService.IsBookExist(id))
            {
                return BadRequest("Book does not exist");
            }

            ValidationResult results = _validator.Validate(request);

            if (!results.IsValid)
            {
                return BadRequest(results.Errors);
            }

            var bookId = await _booksService.UpdateBook(id, request.Title, request.Description, request.Price);
            return Ok(bookId);
        }
        [HttpDelete("{id:guid}")]
        [Authorize]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<Guid>> DeleteBook(Guid id)
        {
            if (!await _booksService.IsBookExist(id))
            {
                return BadRequest("Book does not exist");
            }

            var bookId = await _booksService.DeleteBook(id);
            return Ok(bookId);
        }
    }
}
