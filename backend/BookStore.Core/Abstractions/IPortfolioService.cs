namespace BookStore.Application.Services
{
    public interface IPortfolioService
    { 
        Task<Task> AddToPortfolioAsync(string userId, Guid bookId);
    }
}