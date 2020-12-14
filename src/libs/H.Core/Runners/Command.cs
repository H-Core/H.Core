using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public class Command : CommandBase, ICommand
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        private Action<string> Action { get; }

        #endregion
        
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="action"></param>
        public Command(string prefix, Action<string> action)
        {
            Prefix = prefix ?? throw new ArgumentNullException(nameof(prefix));
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task RunAsync(string arguments, CancellationToken cancellationToken = default)
        {
            OnRunning(arguments);
            
            Action(arguments);

            OnRan(arguments);

            return Task.FromResult(false);
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
