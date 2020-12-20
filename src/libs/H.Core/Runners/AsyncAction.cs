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
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static AsyncAction WithCommand(
            string name,
            Func<ICommand, CancellationToken, Task<IValue>> action,
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
        public static AsyncAction WithCommand(
            string name,
            Func<ICommand, CancellationToken, Task> action,
            string? description = null,
            bool isInternal = false)
        {
            return WithCommand(name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                (command, cancellationToken) =>
                {
                    action?.Invoke(command, cancellationToken);

                    return Task.FromResult<IValue>(Value.Empty);
                },
                description,
                isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static AsyncAction WithData(
            string name,
            Func<byte[], CancellationToken, Task> action,
            string? description = null,
            bool isInternal = false)
        {
            return WithCommand(name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                (command, cancellationToken) =>
                    action?.Invoke(command.Value.Data, cancellationToken) ?? Task.FromResult(false),
                description,
                isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static AsyncAction WithData(
            string name,
            Func<byte[], CancellationToken, Task<IValue>> action,
            string? description = null,
            bool isInternal = false)
        {
            return WithCommand(name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                (command, cancellationToken) =>
                    action?.Invoke(command.Value.Data, cancellationToken) ?? Task.FromResult<IValue>(Value.Empty),
                description,
                isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static AsyncAction WithArguments(
            string name, 
            Func<string[], CancellationToken, Task> action,
            string? description = null,
            bool isInternal = false)
        {
            return WithCommand(name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                (command, cancellationToken) =>
                    action?.Invoke(command.Value.Arguments, cancellationToken) ?? Task.FromResult(false),
                description,
                isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static AsyncAction WithArguments(
            string name,
            Func<string[], CancellationToken, Task<IValue>> action,
            string? description = null,
            bool isInternal = false)
        {
            return WithCommand(name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                (command, cancellationToken) =>
                    action?.Invoke(command.Value.Arguments, cancellationToken) ?? Task.FromResult<IValue>(Value.Empty),
                description,
                isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static AsyncAction WithSingleArgument(
            string name, 
            Func<string, CancellationToken, Task> action,
            string? description = null,
            bool isInternal = false)
        {
            return WithCommand(name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                (command, cancellationToken) =>
                    action?.Invoke(command.Value.Argument, cancellationToken) ?? Task.FromResult(false),
                description,
                isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static AsyncAction WithSingleArgument(
            string name,
            Func<string, CancellationToken, Task<IValue>> action,
            string? description = null,
            bool isInternal = false)
        {
            return WithCommand(name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                (command, cancellationToken) =>
                    action?.Invoke(command.Value.Argument, cancellationToken) ?? Task.FromResult<IValue>(Value.Empty),
                description,
                isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static AsyncAction WithoutArguments(
            string name,
            Func<CancellationToken, Task> action,
            string? description = null,
            bool isInternal = false)
        {
            return WithCommand(name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                (_, cancellationToken) =>
                    action?.Invoke(cancellationToken) ?? Task.FromResult(false),
                description,
                isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static AsyncAction WithoutArguments(
            string name,
            Func<CancellationToken, Task<IValue>> action,
            string? description = null,
            bool isInternal = false)
        {
            return WithCommand(name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                (_, cancellationToken) =>
                    action?.Invoke(cancellationToken) ?? Task.FromResult<IValue>(Value.Empty),
                description,
                isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static AsyncAction WithCommand(
            string name,
            Func<ICommand, Task<IValue>> action,
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
        public static AsyncAction WithCommand(
            string name,
            Func<ICommand, Task> action,
            string? description = null,
            bool isInternal = false)
        {
            return WithCommand(name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                command =>
                {
                    action?.Invoke(command);

                    return Task.FromResult<IValue>(Value.Empty);
                },
                description,
                isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static AsyncAction WithData(
            string name,
            Func<byte[], Task> action,
            string? description = null,
            bool isInternal = false)
        {
            return WithCommand(name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                command =>
                    action?.Invoke(command.Value.Data) ?? Task.FromResult(false),
                description,
                isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static AsyncAction WithData(
            string name,
            Func<byte[], Task<IValue>> action,
            string? description = null,
            bool isInternal = false)
        {
            return WithCommand(name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                command =>
                    action?.Invoke(command.Value.Data) ?? Task.FromResult<IValue>(Value.Empty),
                description,
                isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static AsyncAction WithArguments(
            string name,
            Func<string[], Task> action,
            string? description = null,
            bool isInternal = false)
        {
            return WithCommand(name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                command =>
                    action?.Invoke(command.Value.Arguments) ?? Task.FromResult(false),
                description,
                isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static AsyncAction WithArguments(
            string name,
            Func<string[], Task<IValue>> action,
            string? description = null,
            bool isInternal = false)
        {
            return WithCommand(name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                command =>
                    action?.Invoke(command.Value.Arguments) ?? Task.FromResult<IValue>(Value.Empty),
                description,
                isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static AsyncAction WithSingleArgument(
            string name,
            Func<string, Task> action,
            string? description = null,
            bool isInternal = false)
        {
            return WithCommand(name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                command =>
                    action?.Invoke(command.Value.Argument) ?? Task.FromResult(false),
                description,
                isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static AsyncAction WithSingleArgument(
            string name,
            Func<string, Task<IValue>> action,
            string? description = null,
            bool isInternal = false)
        {
            return WithCommand(name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                command =>
                    action?.Invoke(command.Value.Argument) ?? Task.FromResult<IValue>(Value.Empty),
                description,
                isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static AsyncAction WithoutArguments(
            string name,
            Func<Task> action,
            string? description = null,
            bool isInternal = false)
        {
            return WithCommand(name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                _ =>
                    action?.Invoke() ?? Task.FromResult(false),
                description,
                isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <returns></returns>
        public static AsyncAction WithoutArguments(
            string name,
            Func<Task<IValue>> action,
            string? description = null,
            bool isInternal = false)
        {
            return WithCommand(name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                _ =>
                    action?.Invoke() ?? Task.FromResult<IValue>(Value.Empty),
                description,
                isInternal);
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        private Func<ICommand, CancellationToken, Task<IValue>> Action { get; }
        
        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AsyncAction(
            string name, 
            Func<ICommand, CancellationToken, Task<IValue>> action,
            string? description = null,
            bool isInternal = false) : 
            base(name)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            Description = description ?? string.Empty;
            IsInternal = isInternal;

            IsCancellable = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        public AsyncAction(
            string name, 
            Func<ICommand, Task<IValue>> action,
            string? description = null,
            bool isInternal = false) :
            this(
                name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                (command, _) =>
                    action?.Invoke(command) ?? Task.FromResult<IValue>(Value.Empty),
                description,
                isInternal)
        {
            IsCancellable = false;
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
        public override async Task<IValue> RunAsync(ICommand command, CancellationToken cancellationToken = default)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));

            OnRunning(command);

            var output = IsCancellable
                ? await Action(command, cancellationToken)
                    .ConfigureAwait(false)
                : await Task.Run(() => Action(command, cancellationToken), cancellationToken)
                    .ConfigureAwait(false);
            
            OnRan(command);

            return output;
        }

        #endregion
    }
}
