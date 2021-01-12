using System;
using System.Linq;

namespace H.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class ValueExtensions
    {
        /// <summary>
        /// Returns new value, contains concatenated <see cref="IValue.Arguments"/> and <seealso cref="IValue.Data"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="secondValue"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IValue Merge(this IValue value, IValue secondValue)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));
            secondValue = secondValue ?? throw new ArgumentNullException(nameof(secondValue));

            return new Value(
                value.Data.Concat(secondValue.Data).ToArray(),
                value.Arguments.Concat(secondValue.Arguments).ToArray()
                );
        }
    }
}
