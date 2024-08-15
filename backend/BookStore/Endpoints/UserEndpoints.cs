using BookStore.Application.Services;
using BookStore.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BookStore.Endpoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);
            app.MapPost("login", Login);

            return app;
        }

        private static async Task<IResult> Register(RegisterUserRequest request,UserService userService)
        {
            await userService.Register(request.UserName, request.Email, request.Password);

            return Results.Ok();
        }

        private static async Task<IResult> Login(LoginUserRequest request, UserService userService)
        {
            var token = userService.Login(request.Password,request.Email);

            return Results.Ok(token);
        }
    }
}
