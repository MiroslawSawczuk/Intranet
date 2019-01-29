using System;
using System.Collections.Generic;

namespace Intranet.Authentication.Tokens
{
    public interface ITokenBuilder
    {
        string BuildToken(string id, string email);
        string BuildToken(string id, string email, out DateTime expiration);
    }
}
