using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Recognizers
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class StreamingRecognition : IStreamingRecognition
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string Result { get; protected set; } = string.Empty;

        #endregion
        
        #region Events

        /// <summary>
        /// Before <see cref="StopAsync"/> call.
        /// </summary>
        public event EventHandler? Stopping;
        
        /// <summary>
        /// After <see cref="StopAsync"/> call.
        /// </summary>
        public event EventHandler<string>? Stopped;
        
        /// <summary>
        /// 
        /// </summary>

        public event EventHandler<string>? PreviewReceived;

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
        protected void OnStopped(string value)
        {
            Stopped?.Invoke(this, value);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected void OnPreviewReceived(string value)
        {
            PreviewReceived?.Invoke(this, value);
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
        public abstract Task<string> StopAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        public virtual void Dispose()
        {
        }

        #endregion
    }
}
