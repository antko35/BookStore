using BookStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Entities
{
    public class UserBookEntity
    {
        public Guid UserId {  get; set; }
        public UserEntity User { get; set; } = new UserEntity();
        public Guid BookId { get; set; }
        public BookEntity Book { get; set; } = new BookEntity();

    }
}
