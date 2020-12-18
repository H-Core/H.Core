using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProcess
    {
        #region Methods
        
        /// <summary>
        /// Stops process and waits completion.
        /// </summary>
        Task StopAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Waits stop call.
        /// </summary>
        Task WaitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Initialize process.
        /// </summary>
        void Initialize(Func<Task> func, CancellationToken cancellationToken = default);

        #endregion
    }
    
    /// <summary>
    /// 
    /// </summary>
    public interface IProcess<T>
    {
        #region Methods

        /// <summary>
        /// Stops process and waits completion.
        /// </summary>
        Task<T> StopAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Waits stop call.
        /// </summary>
        Task WaitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Initialize process.
        /// </summary>
        void Initialize(Func<Task<T>> func, CancellationToken cancellationToken = default);

        #endregion
    }
}
