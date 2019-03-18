using Intranet.Users.Enums;

namespace Intranet.Authentication.Tokens
{
    public interface ITokenUser
    {
        string Id { get; }
        string Email { get; }
        string Role { get; }
    }
}
