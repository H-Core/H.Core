using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Utilities;

namespace H.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEventArgs"></typeparam>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Serializable]
    public delegate Task AsyncEventHandler<in TEventArgs>(object sender, TEventArgs e, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEventArgs"></typeparam>
    /// <typeparam name="TTaskType"></typeparam>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Serializable]
    public delegate Task<TTaskType> AsyncEventHandler<in TEventArgs, TTaskType>(object sender, TEventArgs e, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    public static class AsyncEventHandlerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEventArgs"></typeparam>
        /// <param name="handlers"></param>
        /// <param name="source"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task InvokeAsync<TEventArgs>(
            this AsyncEventHandler<TEventArgs>? handlers, 
            object source, 
            TEventArgs args,
            CancellationToken cancellationToken = default)
        {
            if (handlers != null)
            {
                return Task.WhenAll(
                    handlers
                        .GetInvocationList()
                        .OfType<AsyncEventHandler<TEventArgs>>()
                        .Select(func => func(source, args, cancellationToken))
                    );
            }

            return Task.FromResult(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEventArgs"></typeparam>
        /// <typeparam name="TTaskType"></typeparam>
        /// <param name="handlers"></param>
        /// <param name="source"></param>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<TTaskType[]> InvokeAsync<TEventArgs, TTaskType>(
            this AsyncEventHandler<TEventArgs, TTaskType>? handlers,
            object source,
            TEventArgs args,
            CancellationToken cancellationToken = default)
        {
            if (handlers != null)
            {
                return Task.WhenAll(
                    handlers
                        .GetInvocationList()
                        .OfType<AsyncEventHandler<TEventArgs, TTaskType>>()
                        .Select(func => func(source, args, cancellationToken))
                );
            }

            return Task.FromResult(EmptyArray<TTaskType>.Value);
        }
    }
}
