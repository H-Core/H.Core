using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Recorders
{
    public interface IRecorder : IModule
    {
        #region Properties

        bool IsInitialized { get; }
        bool IsStarted { get; }
        byte[]? RawData { get; }
        byte[]? WavData { get; }
        byte[]? WavHeader { get; }

        #endregion

        #region Events

        event EventHandler? Started;
        event EventHandler<RecorderEventArgs>? Stopped;
        event EventHandler<RecorderEventArgs>? RawDataReceived;

        #endregion

        #region Methods

        Task InitializeAsync(CancellationToken cancellationToken = default);
        Task StartAsync(CancellationToken cancellationToken = default);
        Task StopAsync(CancellationToken cancellationToken = default);

        #endregion
    }
}
