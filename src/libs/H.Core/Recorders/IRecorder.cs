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
        
        /// <summary>
        /// 
        /// </summary>
        bool IsStarted { get; }
        
        /// <summary>
        /// 
        /// </summary>
        byte[] RawData { get; }
        
        /// <summary>
        /// 
        /// </summary>
        byte[] WavData { get; }
        
        /// <summary>
        /// 
        /// </summary>
        byte[] WavHeader { get; }

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        event EventHandler? Started;
        
        /// <summary>
        /// 
        /// </summary>
        event EventHandler? Stopped;
        
        /// <summary>
        /// 
        /// </summary>
        event EventHandler<byte[]>? RawDataReceived;

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task InitializeAsync(CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Calls InitializeAsync if recorder is not initialized.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task StartAsync(CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task StopAsync(CancellationToken cancellationToken = default);

        #endregion
    }
}
