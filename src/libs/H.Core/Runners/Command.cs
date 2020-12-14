using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public class Command : CommandBase
    {
        #region Static methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Command WithSingleArgument(string name, Action<string> action)
        {
            return new (name, arguments => action.Invoke(arguments.FirstOrDefault() ?? string.Empty));
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        private Action<string[]> Action { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public Command(string name, Action<string[]> action)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
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
        public override Task RunAsync(string[] arguments, CancellationToken cancellationToken = default)
        {
            OnRunning(arguments);
            
            Action(arguments);

            OnRan(arguments);

            return Task.FromResult(false);
        }

        #endregion

    }
}
