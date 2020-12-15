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
        private Func<string[], CancellationToken, Task> Action { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public AsyncAction(string name, Func<string[], CancellationToken, Task> action) : base(name)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));

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

            Action = (arguments, cancellationToken) =>
                action(string.Join(" ", arguments), cancellationToken);
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

            Action = (_, cancellationToken) => action(cancellationToken);
            IsCancellable = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public AsyncAction(string name, Func<string[], Task> action) : base(name)
        {
            action = action ?? throw new ArgumentNullException(nameof(action));

            Action = (arguments, _) => action(arguments);
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

            Action = (arguments, _) => action(string.Join(" ", arguments));
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

            Action = (_, _) => action();
            IsCancellable = false;
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
