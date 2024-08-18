namespace BookStore.DataAccess.Repositories
{
    public class AuthorizationOptions
    {
        public RolePermissions[] RolePermissions { get; set; } = new RolePermissions[0];
    }

    public class RolePermissions
    {
        public string Role { get; set; } = string.Empty;

        public string[] Permissions { get; set; } = new string[0];
    }
}