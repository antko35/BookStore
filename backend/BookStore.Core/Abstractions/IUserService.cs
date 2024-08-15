namespace BookStore.Application.Services
{
    public interface IUserService
    {
        Task<string> Login(string password, string email);
        Task Register(string userName, string email, string password);
    }
}