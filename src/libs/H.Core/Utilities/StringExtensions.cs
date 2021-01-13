using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace H.Core.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// CMD like(values in \"\" and \'\' can contain whitespaces) split.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static string[] CmdSplit(this string value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            return Regex
                .Matches(value, "[^\\s\"']+|\"([^\"]*)\"|'([^']*)'")
                .Cast<Match>()
                .Select(match => match.Value.Trim('\"', '\''))
                .ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparison"></param>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static bool IsAny(this string value, StringComparison comparison, params string[] strings)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            return strings
                .Any(@string => string.Equals(value, @string, comparison));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static bool IsAnyOrdinalIgnoreCase(this string value, params string[] strings)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            return value.IsAny(StringComparison.OrdinalIgnoreCase, strings);
        }
    }
}
