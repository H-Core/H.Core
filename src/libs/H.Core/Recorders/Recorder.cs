using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Recorders
{
    /// <summary>
    /// /
    /// </summary>
    public abstract class Recorder : Module, IRecorder
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public bool IsInitialized { get; protected set; }

        #endregion

        #region Events
        
        /// <summary>
        /// 
        /// </summary>

        public event EventHandler<IRecording>? Started;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<IRecording>? Stopped;

        /// <summary>
        /// 
        /// </summary>
        protected void OnStarted(IRecording value)
        {
            Started?.Invoke(this, value);
        }

        /// <summary>
        /// 
        /// </summary>
        protected void OnStopped(IRecording value)
        {
            Stopped?.Invoke(this, value);
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
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task<IRecording> StartAsync(RecordingFormat format, CancellationToken cancellationToken = default);

        #endregion
    }
}
