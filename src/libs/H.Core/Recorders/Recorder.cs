using System;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Utilities;

namespace H.Core.Recorders
{
    /// <summary>
    /// /
    /// </summary>
    public class Recorder : Module, IRecorder
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public bool IsInitialized { get; protected set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool IsStarted { get; protected set; }
        
        /// <summary>
        /// 
        /// </summary>
        public byte[] RawData { get; protected set; } = EmptyArray<byte>.Value;
        
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

        public event EventHandler? Started;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? Stopped;

        /// <summary>
        /// When new partial raw data received.
        /// </summary>
        public event EventHandler<byte[]>? RawDataReceived;

        /// <summary>
        /// 
        /// </summary>
        protected void OnStarted()
        {
            Started?.Invoke(this, EventArgs.Empty);
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
        protected void OnRawDataReceived(byte[] value)
        {
            RawDataReceived?.Invoke(this, value);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            IsInitialized = true;

            return Task.FromResult(false);
        }

        /// <summary>
        /// Calls InitializeAsync if recorder is not initialized.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task StartAsync(CancellationToken cancellationToken = default)
        {
            if (IsStarted)
            {
                return;
            }
            if (!IsInitialized)
            {
                await InitializeAsync(cancellationToken).ConfigureAwait(false);
            }

            IsStarted = true;
            RawData = EmptyArray<byte>.Value;
            WavData = EmptyArray<byte>.Value;

            OnStarted();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task StopAsync(CancellationToken cancellationToken = default)
        {
            if (!IsStarted)
            {
                return Task.FromResult(false);
            }

            IsStarted = false;
            OnStopped();
            
            return Task.FromResult(false);
        }

        #endregion
    }
}
