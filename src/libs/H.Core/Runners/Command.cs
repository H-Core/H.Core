using System;
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
        /// <param name="description"></param>
        /// <param name="isCancellable"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static Command WithArguments(
            string name, 
            Action<string> action, 
            string? description = null, 
            bool isCancellable = false, 
            bool isInternal = false)
        {
            return new(name, action)
            {
                Description = description ?? string.Empty,
                IsCancellable = isCancellable,
                IsInternal = isInternal,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isCancellable"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static Command WithSingleArgument(
            string name, 
            Action<string> action,
            string? description = null,
            bool isCancellable = false,
            bool isInternal = false)
        {
            return new (name, action)
            {
                Description = description ?? string.Empty,
                IsCancellable = isCancellable,
                IsInternal = isInternal,
            };
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
        public Command(string name, Action<string[]> action) : base(name)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public Command(string name, Action<string> action) : 
            this(name, (Action<string[]>)(arguments => action.Invoke(string.Join(" ", arguments))))
        {
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
