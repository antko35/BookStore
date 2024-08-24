using BookStore.Application.Services;
using BookStore.Contracts;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<LoginUserRequest> _loginValidate;
        private readonly IValidator<RegisterUserRequest> _registerValidate;
        public UserController(IUserService userService, IValidator<LoginUserRequest> loginValidate, IValidator<RegisterUserRequest> registerValidate)
        {
            _userService = userService;
            _loginValidate = loginValidate;
            _registerValidate = registerValidate;
        }
        [HttpPost("/register")]
        public async Task<IResult> Register([FromBody] RegisterUserRequest request)
        {
            ValidationResult results = _registerValidate.Validate(request);

            if (!results.IsValid)
            {
                return Results.BadRequest(results.Errors);
            }
            await _userService.Register(request.UserName, request.Email, request.Password);

            return Results.Ok();
        }

        [HttpPost("/login")]
        public async Task<IResult> Login([FromBody] LoginUserRequest request)
        {
            ValidationResult results = _loginValidate.Validate(request);

            if (!results.IsValid)
            {
                return Results.BadRequest(results.Errors);
            }

            var token = await _userService.Login(request.Password, request.Email);

            HttpContext.Response.Cookies.Append("cooky_try",token);

            return Results.Ok(token);
        }
        
    }
}
