using BookStore.Core.Models;
using BookStore.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        public PortfolioService(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<List<Book>> GetAllBooks(Guid id)
        {
            var books = await _portfolioRepository.Get(id);
            return books;
        }
        public async Task<Book> AddToPortfolioAsync(string userId, Guid bookId)
        {
            var book = await _portfolioRepository.Add(userId,bookId);
            return book;
        }

        public async Task<Task> Delete(string userId, Guid bookId)
        {
            await _portfolioRepository.Delete(userId, bookId);
            return Task.CompletedTask;
        }

        public async Task<bool> IsInPortfolio(string userId, Guid bookId)
        {
            var exist = await _portfolioRepository.FindPortfolio(userId, bookId);
            return exist;
        }
    }
}
