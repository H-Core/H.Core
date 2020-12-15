using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public class SyncAction : ActionBase
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
        public static SyncAction WithCommand(
            string name,
            Action<ICommand> action,
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
        public static SyncAction WithArguments(
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
        public static SyncAction WithSingleArgument(
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isCancellable"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static SyncAction WithoutArguments(
            string name,
            Action action,
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

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        private Action<ICommand> Action { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public SyncAction(string name, Action<ICommand> action) : base(name)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public SyncAction(string name, Action<string[]> action) : base(name)
        {
            action = action ?? throw new ArgumentNullException(nameof(action));

            Action = command => action(command.Arguments);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public SyncAction(string name, Action<string> action) : base(name)
        {
            action = action ?? throw new ArgumentNullException(nameof(action));

            Action = command => action(command.Argument);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public SyncAction(string name, Action action) : base(name)
        {
            action = action ?? throw new ArgumentNullException(nameof(action));

            Action = _ => action();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task RunAsync(ICommand command, CancellationToken cancellationToken = default)
        {
            OnRunning(command);
            
            Action(command);

            OnRan(command);

            return Task.FromResult(false);
        }

        #endregion
    }
}
