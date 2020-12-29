using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Recorders
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRecorder : IModule
    {
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
        /// <param name="format"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IRecording> StartAsync(AudioFormat format, CancellationToken cancellationToken = default);

        #endregion
    }
}
