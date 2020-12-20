using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Recognizers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStreamingRecognition : IDisposable
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        string Result { get; }

        #endregion

        #region Events

        /// <summary>
        /// Before <see cref="StopAsync"/> call.
        /// </summary>
        event EventHandler? Stopping;

        /// <summary>
        /// After <see cref="StopAsync"/> call.
        /// </summary>
        event EventHandler<string>? Stopped;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<string>? PreviewReceived;

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
        Task<string> StopAsync(CancellationToken cancellationToken = default);
        
        #endregion
    }
}
