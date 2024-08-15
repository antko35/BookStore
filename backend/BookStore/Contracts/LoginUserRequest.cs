using System.ComponentModel.DataAnnotations;

namespace BookStore.Contracts
{
    public record LoginUserRequest(
        [Required] string Email,
        [Required] string Password
        );
}

