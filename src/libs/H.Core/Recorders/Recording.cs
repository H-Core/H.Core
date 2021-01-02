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
        public AudioSettings Settings { get; }
        
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
        public event EventHandler<byte[]>? Stopped;
        
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
        protected void OnStopped(byte[] value)
        {
            Stopped?.Invoke(this, value);
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
        /// <param name="settings"></param>
        protected Recording(AudioSettings settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task<byte[]> StopAsync(CancellationToken cancellationToken = default);

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
