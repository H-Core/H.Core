using System;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Utilities;

namespace H.Core.Recorders
{
    /// <summary>
    /// /
    /// </summary>
    public abstract class Recording : IRecording
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public RecordingFormat Format { get; }
        
        /// <summary>
        /// 
        /// </summary>
        public byte[] Data { get; protected set; } = EmptyArray<byte>.Value;
        
        /// <summary>
        /// 
        /// </summary>
        public byte[] Header { get; protected set; } = EmptyArray<byte>.Value;

        #endregion

        #region Events

        /// <summary>
        /// When new partial raw data received.
        /// </summary>
        public event EventHandler<byte[]>? DataReceived;
        
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? Stopped;
        
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? Disposed;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected void OnDataReceived(byte[] value)
        {
            DataReceived?.Invoke(this, value);
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
        protected void OnDisposed()
        {
            Disposed?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        protected Recording(RecordingFormat format)
        {
            if (format is RecordingFormat.None)
            {
                throw new ArgumentException("Format is None.");
            }
            
            Format = format;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task StopAsync(CancellationToken cancellationToken = default)
        {
            OnStopped();
            
            return Task.FromResult(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Dispose()
        {
            OnDisposed();
        }

        #endregion
    }
}
