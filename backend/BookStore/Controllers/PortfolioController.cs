using BookStore.Application.Services;
using BookStore.DataAccess;
using BookStore.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IBooksService _booksService;
        private readonly BookStoreDbContext _context;
        public PortfolioController(IPortfolioService portfolioService,IBooksService booksService, BookStoreDbContext context)
        {
            _portfolioService = portfolioService;
            _booksService = booksService;
            _context = context;
        }
        [HttpGet("/getMyBooks")]
        public async Task<ActionResult> GetMyBooks()
        {
            string userId = User.Claims.FirstOrDefault(x => x.Type == CustomClaims.UserId).Value;
            var books = await _portfolioService.GetAllBooks(Guid.Parse(userId));
            return Ok(books);
        }
        
        [HttpPost("/addToPortfolio/{bookId:Guid}")]
        public async Task<ActionResult> AddToPortfolio(Guid bookId)
        {
            bool bookExist = await _booksService.IsBookExist(bookId);
            if (!bookExist)
            {
                return BadRequest("Book does not exist");
            }

            string userId = User.Claims.FirstOrDefault(x => x.Type == CustomClaims.UserId).Value;

            if (await _portfolioService.IsInPortfolio(userId, bookId))
            {
                return BadRequest("Already in portfolio");
            }
           
           
            var book = await _portfolioService.AddToPortfolioAsync(userId, bookId);

            // var books = await _portfolioService.AddToPortfolio();
            return Ok(book);
        }

        [HttpDelete("/delete/{bookId:Guid}")]
        public async Task<ActionResult> DeleteFromPortfolio(Guid bookId)
        {
            bool bookExist = await _booksService.IsBookExist(bookId);
            if (!bookExist)
            {
                return BadRequest("Book does not exist");
            }

            string userId = User.Claims.FirstOrDefault(x => x.Type == CustomClaims.UserId).Value;

            if (!await _portfolioService.IsInPortfolio(userId, bookId))
            {
                return BadRequest("Not in portfolio");
            }

            await _portfolioService.Delete(userId, bookId);

            return Ok();
        }
    }
}
