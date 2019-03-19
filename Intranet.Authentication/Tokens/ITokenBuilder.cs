using Intranet.Users.Enums;
using System;

namespace Intranet.Authentication.Tokens
{
    public interface ITokenBuilder
    {
        string BuildToken(string id, string email);
        string BuildToken(string id, string email, Permission role, string tenantId);
        string BuildToken(string id, string email, Permission role, string tenantId, out DateTime expiration);
    }
}
