using BookStore.Core.Models;


namespace BookStore.DataAccess.Repositories
{
    public interface IPortfolioRepository
    {
        Task<Book> Add(string userId, Guid bookId);
        Task Delete(string userId, Guid bookId);
        Task<bool> FindPortfolio(string userId, Guid bookId);
        Task<List<Book>> Get(Guid id);
    }
}