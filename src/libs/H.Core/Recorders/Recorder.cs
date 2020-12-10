using System;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Utilities;

namespace H.Core.Recorders
{
    public class Recorder : Module, IRecorder
    {
        #region Properties

        public bool IsInitialized { get; protected set; }
        public bool IsStarted { get; protected set; }
        public byte[] RawData { get; protected set; } = EmptyArray<byte>.Value;
        public byte[] WavData { get; protected set; } = EmptyArray<byte>.Value;
        public byte[] WavHeader { get; protected set; } = EmptyArray<byte>.Value;

        #endregion

        #region Events

        public event EventHandler? Started;

        public event EventHandler? Stopped;

        /// <summary>
        /// When new partial raw data received.
        /// </summary>
        public event EventHandler<byte[]>? RawDataReceived;

        protected void OnStarted()
        {
            Started?.Invoke(this, EventArgs.Empty);
        }


        protected void OnStopped()
        {
            Stopped?.Invoke(this, EventArgs.Empty);
        }

        protected void OnRawDataReceived(byte[] value)
        {
            RawDataReceived?.Invoke(this, value);
        }

        #endregion

        #region Public methods

        public virtual Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            IsInitialized = true;

            return Task.FromResult(false);
        }

        public virtual async Task StartAsync(CancellationToken cancellationToken = default)
        {
            if (IsStarted)
            {
                throw new InvalidOperationException("Already started");
            }

            IsStarted = true;
            RawData = EmptyArray<byte>.Value;
            WavData = EmptyArray<byte>.Value;

            OnStarted();

            await Task.Delay(TimeSpan.Zero, cancellationToken).ConfigureAwait(false);
        }

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
