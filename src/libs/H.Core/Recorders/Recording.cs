using System;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Utilities;

namespace H.Core.Recorders
{
    /// <summary>
    /// /
    /// </summary>
    public class Recording : IRecording
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public byte[] Data { get; protected set; } = EmptyArray<byte>.Value;
        
        /// <summary>
        /// 
        /// </summary>
        public byte[] WavData { get; protected set; } = EmptyArray<byte>.Value;
        
        /// <summary>
        /// 
        /// </summary>
        public byte[] WavHeader { get; protected set; } = EmptyArray<byte>.Value;

        #endregion

        #region Events
        
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? Stopped;

        /// <summary>
        /// When new partial raw data received.
        /// </summary>
        public event EventHandler<byte[]>? DataReceived;

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
        protected void OnDataReceived(byte[] value)
        {
            DataReceived?.Invoke(this, value);
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
        }

        #endregion
    }
}
