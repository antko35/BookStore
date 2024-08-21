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

        public async Task<Task> AddToPortfolioAsync(string userId, Guid bookId)
        {
            await _portfolioRepository.Add(userId,bookId);
            return Task.CompletedTask;
        }
    }
}
