using System;
using System.Collections.Generic;
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
        public ICollection<AudioSettings> SupportedSettings { get; } = new List<AudioSettings>();

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
        /// <param name="settings"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task<IRecording> StartAsync(AudioSettings? settings = null, CancellationToken cancellationToken = default);

        #endregion
    }
}
