using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
    public static class AsynchronousEventExtensions
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
        public static Task OnAsync<TEventArgs>(
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
    }
}
