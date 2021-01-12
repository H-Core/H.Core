using System;

namespace H.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class CommandExtensions
    {
        /// <summary>
        /// Returns original command, where <see cref="ICommand.Input"/> contains
        /// concatenated <see cref="IValue.Arguments"/> and <seealso cref="IValue.Data"/>.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ICommand WithMergedInput(this ICommand command, IValue value)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));
            value = value ?? throw new ArgumentNullException(nameof(value));

            command.Input = command.Input.Merge(value);
            return command;
        }

        /// <summary>
        /// Returns original command, where <see cref="ICommand.Output"/> contains
        /// concatenated <see cref="IValue.Arguments"/> and <seealso cref="IValue.Data"/>.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ICommand WithMergedOutput(this ICommand command, IValue value)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));
            value = value ?? throw new ArgumentNullException(nameof(value));

            command.Output = command.Output.Merge(value);
            return command;
        }
    }
}
