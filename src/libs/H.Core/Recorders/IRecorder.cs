using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Recorders
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRecorder : IModule
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        ICollection<AudioSettings> SupportedSettings { get; }

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<IRecording>? Started;
        
        /// <summary>
        /// 
        /// </summary>
        event EventHandler<IRecording>? Stopped;

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IRecording> StartAsync(AudioSettings? settings = null, CancellationToken cancellationToken = default);

        #endregion
    }
}
