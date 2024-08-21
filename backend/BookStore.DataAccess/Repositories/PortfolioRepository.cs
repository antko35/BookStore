using BookStore.Core.Models;
using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly BookStoreDbContext _context;
        public PortfolioRepository(BookStoreDbContext context)
        {
            _context = context;
        }
       
        public Task Add()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Book>> Get(Guid id)
        {
            var bookEntities = await _context.UserBooks
                .AsNoTracking()
                .Where(x => x.UserId == id)
                .Include(x => x.Book)  // Включаем связанные книги
                .Select(x => x.Book)  // Извлекаем только книги
                .ToListAsync();

            var books = bookEntities
                .Select(b => Book.Create(b.Id, b.Title, b.Description, b.Price).book)
                .ToList();

            return books;
        }

        public async Task Add(string userId, Guid bookId)
        {
            var potrfolio = new UserBookEntity
            {
                UserId = Guid.Parse(userId),
                BookId = bookId
            };
            await _context.UserBooks.AddAsync(potrfolio);
            await _context.SaveChangesAsync();
        }
    }
}
