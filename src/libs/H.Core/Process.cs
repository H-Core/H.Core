using System;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Utilities;

namespace H.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Process : IProcess
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public ExceptionsBag Exceptions { get; } = new();

        private Task MainTask { get; set; } = Task.FromResult(false);
        private TaskCompletionSource<bool> WaitSource { get; } = new();

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            WaitSource.TrySetResult(true);

            await MainTask.ConfigureAwait(false);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="cancellationToken"></param>
        public void Initialize(Func<Task> func, CancellationToken cancellationToken = default)
        {
            func = func ?? throw new ArgumentNullException(nameof(func));
            
            MainTask = Task.Run(async () =>
            {
                await func().ConfigureAwait(false);
                
                Exceptions.EnsureNoExceptions();
            }, cancellationToken);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public async Task WaitAsync(CancellationToken cancellationToken = default)
        {
            using var registration = cancellationToken.Register(() => WaitSource.TrySetCanceled());

            await WaitSource.Task.ConfigureAwait(false);
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class Process<T> : IProcess<T>
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public ExceptionsBag Exceptions { get; } = new();

        private Task<T> MainTask { get; set; } = Task.FromResult<T>(default!);
        private TaskCompletionSource<bool> WaitSource { get; } = new();

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public async Task<T> StopAsync(CancellationToken cancellationToken = default)
        {
            WaitSource.TrySetResult(true);

            return await MainTask.ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="cancellationToken"></param>
        public void Initialize(Func<Task<T>> func, CancellationToken cancellationToken = default)
        {
            func = func ?? throw new ArgumentNullException(nameof(func));

            MainTask = Task.Run(async () =>
            {
                var result = await func().ConfigureAwait(false);

                Exceptions.EnsureNoExceptions();
                
                return result;
            }, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task WaitAsync(CancellationToken cancellationToken = default)
        {
            using var registration = cancellationToken.Register(() => WaitSource.TrySetCanceled());

            await WaitSource.Task.ConfigureAwait(false);
        }

        #endregion
    }
}
