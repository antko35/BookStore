using BookStore.Application.Services;
using BookStore.DataAccess;
using BookStore.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;
        private readonly BookStoreDbContext _context;
        public PortfolioController(IPortfolioService portfolioService, BookStoreDbContext context)
        {
            _portfolioService = portfolioService;
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
            string userId = User.Claims.FirstOrDefault(x => x.Type == CustomClaims.UserId).Value;
            await _portfolioService.AddToPortfolioAsync(userId, bookId);

            // var books = await _portfolioService.AddToPortfolio();
            return Ok();
        }

        [HttpDelete("/delete/{bookId:Guid}")]
        public async Task<ActionResult> DeleteFromPortfolio(Guid bookId)
        {
            string userId = User.Claims.FirstOrDefault(x => x.Type == CustomClaims.UserId).Value;
            await _portfolioService.Delete(userId, bookId);
            return Ok();
        }
    }
}
