using System;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Utilities;

namespace H.Core.Recognizers
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class StreamingRecognition : DisposableObject, IStreamingRecognition
    {
        #region Events

        /// <summary>
        /// Before <see cref="StopAsync"/> call.
        /// </summary>
        public event EventHandler? Stopping;
        
        /// <summary>
        /// After <see cref="StopAsync"/> call.
        /// </summary>
        public event EventHandler? Stopped;
        
        /// <summary>
        /// 
        /// </summary>

        public event EventHandler<string>? PartialResultsReceived;
        
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<string>? FinalResultsReceived;

        /// <summary>
        /// 
        /// </summary>
        protected void OnStopping()
        {
            Stopping?.Invoke(this, EventArgs.Empty);
        }
        
        /// <summary>
        /// 
        /// </summary>
        protected void OnStopped()
        {
            Stopped?.Invoke(this, EventArgs.Empty);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected void OnPartialResultsReceived(string value)
        {
            PartialResultsReceived?.Invoke(this, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected void OnFinalResultsReceived(string value)
        {
            FinalResultsReceived?.Invoke(this, value);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task WriteAsync(byte[] bytes, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task StopAsync(CancellationToken cancellationToken = default);

        #endregion
    }
}
