using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Utilities;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public static class RunnerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="runner"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static async Task<string> WaitNextCommandAsync(
            this Runner runner, 
            TimeSpan timeout, 
            CancellationToken cancellationToken = default)
        {
            runner = runner ?? throw new ArgumentNullException(nameof(runner));

            var value = await runner.RunAsync(
                new Command("start-record", $"{timeout.TotalMilliseconds}"), cancellationToken)
                .ConfigureAwait(false);
            
            return value.First().Argument;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runner"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="additionalAccepts"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<bool> WaitAccept(
            this Runner runner, 
            TimeSpan timeout,
            CancellationToken cancellationToken = default, 
            params string[] additionalAccepts)
        {
            runner = runner ?? throw new ArgumentNullException(nameof(runner));
            
            var command = await runner.WaitNextCommandAsync(timeout, cancellationToken)
                .ConfigureAwait(false);

            var defaultAccepts = new List<string> { "yes", "да", "согласен" };
            defaultAccepts.AddRange(additionalAccepts);

            return command.IsAnyOrdinalIgnoreCase(defaultAccepts.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runner"></param>
        /// <param name="message"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="additionalAccepts"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<bool> WaitAccept(
            this Runner runner, 
            string message, 
            TimeSpan timeout,
            CancellationToken cancellationToken = default, 
            params string[] additionalAccepts)
        {
            runner = runner ?? throw new ArgumentNullException(nameof(runner));

            await runner.SayAsync(message, cancellationToken).ConfigureAwait(false);

            return await runner.WaitAccept(message, timeout, cancellationToken, additionalAccepts)
                .ConfigureAwait(false);
        }
    }
}
