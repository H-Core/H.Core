using System;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Utilities;

namespace H.Core.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class StreamingRecognition : DisposableObject, IStreamingRecognition
    {
        #region Events
        
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
