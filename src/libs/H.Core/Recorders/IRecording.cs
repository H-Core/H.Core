﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Recorders
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRecording : IDisposable
    {
        #region Properties

        /// <summary>
        /// AudioSettings.
        /// </summary>
        AudioSettings Settings { get; }
        
        /// <summary>
        /// Data.
        /// </summary>
        byte[] Data { get; }

        /// <summary>
        /// Header bytes.
        /// </summary>
        byte[] Header { get; }

        #endregion

        #region Events

        /// <summary>
        /// After <see cref="StopAsync"/> call.
        /// </summary>
        event EventHandler<byte[]>? Stopped;
        
        /// <summary>
        /// After <see cref="IDisposable.Dispose"/> call.
        /// </summary>
        event EventHandler? Disposed;

        /// <summary>
        /// Raw data received.
        /// </summary>
        event EventHandler<byte[]>? DataReceived;

        #endregion

        #region Methods
        
        /// <summary>
        /// Stops recording.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<byte[]> StopAsync(CancellationToken cancellationToken = default);
        
        #endregion
    }
}
