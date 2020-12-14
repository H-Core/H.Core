using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public class AsyncCommand : CommandBase, ICommand
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        private Func<string, Task> Action { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="action"></param>
        public AsyncCommand(string prefix, Func<string, Task> action)
        {
            Prefix = prefix ?? throw new ArgumentNullException(nameof(prefix));
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
        public async Task RunAsync(string arguments, CancellationToken cancellationToken = default)
        {
            await Action(arguments).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public ICall PrepareCall(string arguments)
        {
            return new Call(this, arguments);
        }

        #endregion

    }
}
