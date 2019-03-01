using System;
using System.Collections.Generic;
using System.Text;

namespace Intranet.Logic.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string input) => string.IsNullOrEmpty(input);

        public static bool IsNullOrWhiteSpace(this string input) => string.IsNullOrWhiteSpace(input);
    }
}
