using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public class AsyncCommand : CommandBase
    {
        #region Static methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static AsyncCommand WithSingleArgument(string name, Func<string, CancellationToken, Task> action)
        {
            return new(
                name, 
                (arguments, cancellationToken) => 
                    action.Invoke(arguments.FirstOrDefault() ?? string.Empty, cancellationToken));
        }

        #endregion
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        private Func<string[], CancellationToken, Task> Action { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public AsyncCommand(string name, Func<string[], CancellationToken, Task> action)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Action = action ?? throw new ArgumentNullException(nameof(action));

            IsCancellable = true;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task RunAsync(string[] arguments, CancellationToken cancellationToken = default)
        {
            OnRunning(arguments);
            
            await Action(arguments, cancellationToken).ConfigureAwait(false);
            
            OnRan(arguments);
        }

        #endregion

    }
}
