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
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        bool IsInitialized { get; }

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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task InitializeAsync(CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IRecording> StartAsync(CancellationToken cancellationToken = default);

        #endregion
    }
}
