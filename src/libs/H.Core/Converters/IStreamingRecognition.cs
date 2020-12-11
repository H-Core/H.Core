using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStreamingRecognition : IDisposable
    {
        #region Events

        /// <summary>
        /// Before <see cref="StopAsync"/> call.
        /// </summary>
        event EventHandler? Stopping;

        /// <summary>
        /// After <see cref="StopAsync"/> call.
        /// </summary>
        event EventHandler? Stopped;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<string>? PartialResultsReceived;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<string>? FinalResultsReceived;

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task WriteAsync(byte[] bytes, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task StopAsync(CancellationToken cancellationToken = default);
        
        #endregion
    }
}
