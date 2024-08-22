using BookStore.Core.Models;

namespace BookStore.Application.Services
{
    public interface IPortfolioService
    { 
        Task<Task> AddToPortfolioAsync(string userId, Guid bookId);
        Task<Task> Delete(string userId, Guid bookId);
        Task<List<Book>> GetAllBooks(Guid id);
    }
}