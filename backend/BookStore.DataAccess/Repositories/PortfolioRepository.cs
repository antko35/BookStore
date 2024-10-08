﻿using BookStore.Core.Mappers;
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

        public async Task<Book> Add(string userId, Guid bookId)
        {
            var Id = Guid.Parse(userId);
            var potrfolio = new UserBookEntity
            {
                UserId = Id,
                BookId = bookId,
                User = _context.User.FirstOrDefault(u => u.Id == Id),
                Book = _context.Books.FirstOrDefault(b => b.Id == bookId)
            };

            await _context.UserBooks.AddAsync(potrfolio);
            await _context.SaveChangesAsync();
            var book = Book.Create(potrfolio.Book.Id, potrfolio.Book.Title, potrfolio.Book.Description, potrfolio.Book.Price).book;
            return book;
        }

        public async Task Delete(string userId, Guid bookId)
        {
            var Id = Guid.Parse(userId);
            await _context.UserBooks
                .Where(u => u.UserId == Id && u.BookId == bookId)
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<bool> FindPortfolio(string userId, Guid bookId)
        {
            var Id = Guid.Parse(userId);
            var exist = await _context.UserBooks.AnyAsync(k => k.UserId == Id && k.BookId == bookId);
            return exist;
        }
    }
}
