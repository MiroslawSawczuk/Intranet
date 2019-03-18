using Intranet.Users.Enums;
using System;

namespace Intranet.Authentication.Tokens
{
    public interface ITokenBuilder
    {
        string BuildToken(string id, string email);
        string BuildToken(string id, string email, Permission role);
        string BuildToken(string id, string email, Permission role, out DateTime expiration);
    }
}
