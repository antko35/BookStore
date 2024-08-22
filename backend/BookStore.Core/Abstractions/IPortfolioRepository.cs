using BookStore.Core.Models;


namespace BookStore.DataAccess.Repositories
{
    public interface IPortfolioRepository
    {
        Task Add(string userId, Guid bookId);
        Task Delete(string userId, Guid bookId);
        Task<List<Book>> Get(Guid id);
    }
}