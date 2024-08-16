using BookStore.Application.Services;
using BookStore.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
         private readonly HttpContext _context;
        public UserController(IUserService userService)
        {
            _userService = userService;
           
        }
        [HttpPost("/register")]
        public async Task<IResult> Register([FromBody] RegisterUserRequest request)
        {
            await _userService.Register(request.UserName, request.Email, request.Password);

            return Results.Ok();
        }

        [HttpPost("/login")]
        public async Task<IResult> Login([FromBody] LoginUserRequest request)
        {
            var token = await _userService.Login(request.Password, request.Email);

            HttpContext.Response.Cookies.Append("cooky_try",token);

            return Results.Ok(token);
        }
    }
}
