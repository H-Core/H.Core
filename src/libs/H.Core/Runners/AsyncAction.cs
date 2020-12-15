using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public class AsyncAction : ActionBase
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
        public static AsyncAction WithCommand(
            string name,
            Func<ICommand, CancellationToken, Task> action,
            string? description = null,
            bool isCancellable = true,
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
        public static AsyncAction WithArguments(
            string name, 
            Func<string[], CancellationToken, Task> action,
            string? description = null,
            bool isCancellable = true,
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
        public static AsyncAction WithSingleArgument(
            string name, 
            Func<string, CancellationToken, Task> action,
            string? description = null,
            bool isCancellable = true,
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
        public static AsyncAction WithoutArguments(
            string name,
            Func<CancellationToken, Task> action,
            string? description = null,
            bool isCancellable = true,
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
        public static AsyncAction WithCommandAndWithoutToken(
            string name,
            Func<ICommand, Task> action,
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
        public static AsyncAction WithArgumentsAndWithoutToken(
            string name,
            Func<string[], Task> action,
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
        public static AsyncAction WithSingleArgumentAndWithoutToken(
            string name,
            Func<string, Task> action,
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
        public static AsyncAction WithoutArgumentsAndToken(
            string name,
            Func<Task> action,
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
        private Func<ICommand, CancellationToken, Task> Action { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public AsyncAction(string name, Func<ICommand, CancellationToken, Task> action) : base(name)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));

            IsCancellable = true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public AsyncAction(string name, Func<string[], CancellationToken, Task> action) : base(name)
        {
            action = action ?? throw new ArgumentNullException(nameof(action));

            Action = (command, cancellationToken) =>
                action(command.Arguments, cancellationToken);
            IsCancellable = true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public AsyncAction(string name, Func<string, CancellationToken, Task> action) : base(name)
        {
            action = action ?? throw new ArgumentNullException(nameof(action));

            Action = (command, cancellationToken) =>
                action(command.Argument, cancellationToken);
            IsCancellable = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public AsyncAction(string name, Func<CancellationToken, Task> action) : base(name)
        {
            action = action ?? throw new ArgumentNullException(nameof(action));

            Action = (_, cancellationToken) => 
                action(cancellationToken);
            IsCancellable = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public AsyncAction(string name, Func<ICommand, Task> action) : base(name)
        {
            action = action ?? throw new ArgumentNullException(nameof(action));

            Action = (command, _) => 
                action(command);
            IsCancellable = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public AsyncAction(string name, Func<string[], Task> action) : base(name)
        {
            action = action ?? throw new ArgumentNullException(nameof(action));

            Action = (command, _) => 
                action(command.Arguments);
            IsCancellable = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public AsyncAction(string name, Func<string, Task> action) : base(name)
        {
            action = action ?? throw new ArgumentNullException(nameof(action));

            Action = (command, _) => 
                action(command.Argument);
            IsCancellable = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public AsyncAction(string name, Func<Task> action) : base(name)
        {
            action = action ?? throw new ArgumentNullException(nameof(action));

            Action = (_, _) =>
                action();
            IsCancellable = false;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task RunAsync(ICommand command, CancellationToken cancellationToken = default)
        {
            OnRunning(command);
            
            await Action(command, cancellationToken).ConfigureAwait(false);
            
            OnRan(command);
        }

        #endregion

    }
}
