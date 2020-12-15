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
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static SyncAction WithCommand(
            string name,
            Action<ICommand> action,
            string? description = null,
            bool isInternal = false)
        {
            return new(name, action, description, isInternal);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static SyncAction WithArguments(
            string name, 
            Action<string> action, 
            string? description = null, 
            bool isInternal = false)
        {
            return new(name, action, description, isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static SyncAction WithSingleArgument(
            string name, 
            Action<string> action,
            string? description = null,
            bool isInternal = false)
        {
            return new (name, action, description, isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static SyncAction WithoutArguments(
            string name,
            Action action,
            string? description = null,
            bool isInternal = false)
        {
            return new(name, action, description, isInternal);
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
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        public SyncAction(
            string name, 
            Action<ICommand> action,
            string? description = null,
            bool isInternal = false) : 
            base(name)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            Description = description ?? string.Empty;
            IsInternal = isInternal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        public SyncAction(
            string name, 
            Action<string[]> action,
            string? description = null,
            bool isInternal = false) : 
            this(
                name, 
                // ReSharper disable once ConstantConditionalAccessQualifier
                command => action?.Invoke(command.Arguments), 
                description, 
                isInternal)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        public SyncAction(
            string name, 
            Action<string> action,
            string? description = null,
            bool isInternal = false) :
            this(
                name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                command => action?.Invoke(command.Argument),
                description,
                isInternal)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        public SyncAction(
            string name, 
            Action action,
            string? description = null,
            bool isInternal = false) :
            this(
                name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                (Action<ICommand>)(_ => action?.Invoke()),
                description,
                isInternal)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public override async Task RunAsync(ICommand command, CancellationToken cancellationToken = default)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));
            
            OnRunning(command);

            await Task.Run(() => Action(command), cancellationToken)
                .ConfigureAwait(false);

            OnRan(command);
        }

        #endregion
    }
}
