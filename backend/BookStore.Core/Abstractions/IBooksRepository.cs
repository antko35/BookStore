using BookStore.Core.Models;

namespace BookStore.DataAccess.Repositories
{
    public interface IBooksRepository
    {
        Task<Guid> Create(Book book);
        Task<Guid> Delete(Guid id);
        Task<List<Book>> Get();
        Task<bool> IsExist(Guid Id);
        Task<Guid> Update(Guid id, string title, string decsription, decimal price);
    }
}