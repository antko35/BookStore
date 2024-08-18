using BookStore.DataAccess.Configurations;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BookStore.DataAccess
{
    public class BookStoreDbContext : DbContext
    {
        // Поле для хранения настроек авторизации
        private readonly AuthorizationOptions _authOptions;

        // Конструктор с внедрением зависимостей
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options, IOptions<AuthorizationOptions> authOptions)
            : base(options)
        {
            _authOptions = authOptions.Value;
        }

        // Определение DbSet для сущностей
        public DbSet<UserEntity> User { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<BookEntity> Books { get; set; }

        // Метод для настройки модели данных
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Применение конфигураций из сборки
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookStoreDbContext).Assembly);

            // Применение специальной конфигурации для RolePermission
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(_authOptions));
        }
    }
}
