﻿using System;
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
            Func<ICommand, CancellationToken, Task> action,
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
        public static AsyncAction WithArguments(
            string name, 
            Func<string[], CancellationToken, Task> action,
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
        public static AsyncAction WithSingleArgument(
            string name, 
            Func<string, CancellationToken, Task> action,
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
        public static AsyncAction WithoutArguments(
            string name,
            Func<CancellationToken, Task> action,
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
        public static AsyncAction WithCommandAndWithoutToken(
            string name,
            Func<ICommand, Task> action,
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
        public static AsyncAction WithArgumentsAndWithoutToken(
            string name,
            Func<string[], Task> action,
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
        public static AsyncAction WithSingleArgumentAndWithoutToken(
            string name,
            Func<string, Task> action,
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
        public static AsyncAction WithoutArgumentsAndToken(
            string name,
            Func<Task> action,
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
        private Func<ICommand, CancellationToken, Task> Action { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        public AsyncAction(
            string name, 
            Func<ICommand, CancellationToken, Task> action,
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
            Func<string[], CancellationToken, Task> action,
            string? description = null,
            bool isInternal = false) :
            this(
                name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                (command, cancellationToken) =>
                    action?.Invoke(command.Arguments, cancellationToken) ?? Task.FromResult(false),
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
        public AsyncAction(
            string name, 
            Func<string, CancellationToken, Task> action,
            string? description = null,
            bool isInternal = false) :
            this(
                name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                (command, cancellationToken) =>
                    action?.Invoke(command.Argument, cancellationToken) ?? Task.FromResult(false),
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
        public AsyncAction(
            string name, 
            Func<CancellationToken, Task> action,
            string? description = null,
            bool isInternal = false) :
            this(
                name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                (Func<ICommand, CancellationToken, Task>)((_, cancellationToken) =>
                    action?.Invoke(cancellationToken) ?? Task.FromResult(false)),
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
        public AsyncAction(
            string name, 
            Func<ICommand, Task> action,
            string? description = null,
            bool isInternal = false) :
            this(
                name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                (command, _) =>
                    action?.Invoke(command) ?? Task.FromResult(false),
                description,
                isInternal)
        {
            IsCancellable = false;
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
            Func<string[], Task> action,
            string? description = null,
            bool isInternal = false) :
            this(
                name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                (command, _) =>
                    action?.Invoke(command.Arguments) ?? Task.FromResult(false),
                description,
                isInternal)
        {
            IsCancellable = false;
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
            Func<string, Task> action,
            string? description = null,
            bool isInternal = false) :
            this(
                name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                (command, _) =>
                    action?.Invoke(command.Argument) ?? Task.FromResult(false),
                description,
                isInternal)
        {
            IsCancellable = false;
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
            Func<Task> action,
            string? description = null,
            bool isInternal = false) :
            this(
                name,
                // ReSharper disable once ConstantConditionalAccessQualifier
                (Func<ICommand, CancellationToken, Task>)((_, _) =>
                    action?.Invoke() ?? Task.FromResult(false)),
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
