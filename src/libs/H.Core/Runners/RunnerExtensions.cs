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
        /// Calls runner's command.
        /// </summary>
        /// <param name="runner"></param>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<ICommand> CallAsync(
            this IRunner runner,
            ICommand command,
            CancellationToken cancellationToken = default)
        {
            runner = runner ?? throw new ArgumentNullException(nameof(runner));
            command = command ?? throw new ArgumentNullException(nameof(command));

            var call = runner.TryPrepareCall(command) ??
                       throw new InvalidOperationException($"Command is not supported: {command.Name}.");

            return await call.RunAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Calls runner's command.
        /// </summary>
        /// <param name="runner"></param>
        /// <param name="command"></param>
        /// <param name="process"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<ICommand> CallAsync(
            this IRunner runner,
            ICommand command,
            IProcess<ICommand> process,
            CancellationToken cancellationToken = default)
        {
            runner = runner ?? throw new ArgumentNullException(nameof(runner));
            command = command ?? throw new ArgumentNullException(nameof(command));
            process = process ?? throw new ArgumentNullException(nameof(process));

            var call = runner.TryPrepareCall(command) ??
                       throw new InvalidOperationException($"Command is not supported: {command.Name}.");

            return await call.RunAsync(process, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<string> GetNextCommandAsync(this Module module, CancellationToken cancellationToken = default)
        {
            module = module ?? throw new ArgumentNullException(nameof(module));

            var values = await module.RunAsync(new Command("get-next-command"), cancellationToken)
                .ConfigureAwait(false);

            return values
                .FirstOrDefault(value => !string.IsNullOrWhiteSpace(value.Argument))?
                .Argument ?? string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runner"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="additionalAccepts"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<bool> WaitAccept(
            this Runner runner, 
            CancellationToken cancellationToken = default, 
            params string[] additionalAccepts)
        {
            runner = runner ?? throw new ArgumentNullException(nameof(runner));
            
            var command = await runner.GetNextCommandAsync(cancellationToken)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runner"></param>
        /// <param name="text"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static async Task<string[]> SearchAsync(
            this Runner runner,
            string text,
            CancellationToken cancellationToken = default)
        {
            runner = runner ?? throw new ArgumentNullException(nameof(runner));

            var values = await runner.RunAsync(new Command("search", text), cancellationToken)
                .ConfigureAwait(false);

            return values
                .SelectMany(value => value.Arguments)
                .ToArray();
        }
    }
}
