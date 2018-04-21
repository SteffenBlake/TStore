using System;
using System.Text.RegularExpressions;

namespace TStore.Extensions
{
    public static class StringExtensions
    {
        public static string WildCardToRegex(this string value)
        {
            return "^" + Regex.Escape(value).Replace("\\*", ".*") + "$";
        }
    }
}
