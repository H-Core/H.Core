using System;
using System.Collections.Generic;
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
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="separator"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static string[] SplitOnlyFirst(this string? text, char separator)
        {
            text = text ?? throw new ArgumentNullException(nameof(text));

            if (!text.Contains(separator.ToString()))
            {
                return new[] { text };
            }

            var array = text.Split(separator);
            var prefix = array.First();
            var postfix = string.Join(separator.ToString(), array.Skip(1));

            return new[] { prefix, postfix };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="separator"></param>
        /// <param name="quoteSeparator"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static string[] SplitOnlyFirstIgnoreQuote(this string text, char separator, char quoteSeparator = '\"')
        {
            text = text ?? throw new ArgumentNullException(nameof(text));
            
            if (!text.Contains(quoteSeparator.ToString()))
            {
                return text.SplitOnlyFirst(separator);
            }

            var matches = new List<string>();
            var i = 0;
            var replacedText = Regex.Replace(text, $"{quoteSeparator}(.*?){quoteSeparator}", match =>
            {
                matches.Add(match.ToString());
                var name = GetVariableName(i);
                i++;

                return name;
            });

            var values = replacedText.SplitOnlyFirst(separator);

            for (var j = 0; j < i; j++)
            {
                for (var k = 0; k < values.Length; k++)
                {
                    values[k] = values[k].Replace(GetVariableName(j), matches[j]);
                }
            }

            return values;
        }

        private static string GetVariableName(int i) => $"$VARIABLE{i}$";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="comparison"></param>
        /// <param name="otherStrings"></param>
        /// <returns></returns>
        public static bool IsAny(this string? text, StringComparison comparison, params string[] otherStrings)
        {
            foreach (var str in otherStrings)
            {
                if (string.Equals(text, str, comparison))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="otherStrings"></param>
        /// <returns></returns>
        public static bool IsAny(this string text, params string[] otherStrings) =>
            text.IsAny(StringComparison.CurrentCulture, otherStrings);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="otherStrings"></param>
        /// <returns></returns>
        public static bool IsAnyOrdinalIgnoreCase(this string? text, params string[] otherStrings) =>
            text.IsAny(StringComparison.OrdinalIgnoreCase, otherStrings);
    }
}
