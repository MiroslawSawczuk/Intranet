using System;

namespace Intranet.Authentication.DependencyInjection
{
    public class JwtTokenOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public TimeSpan Expiration { get; set; }
    }
}
