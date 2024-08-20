using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Configurations
{
    public class UserBookConfiguration : IEntityTypeConfiguration<UserBookEntity>
    {
        public void Configure(EntityTypeBuilder<UserBookEntity> builder)
        {
            builder.HasKey(r => new { r.UserId, r.BookId });
        } 
    }
}
