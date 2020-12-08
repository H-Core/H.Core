using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core
{
    public interface IStreamingRecognition : IDisposable
    {
        event EventHandler<VoiceActionsEventArgs>? AfterPartialResults;
        event EventHandler<VoiceActionsEventArgs>? AfterFinalResults;

        Task WriteAsync(byte[] bytes, CancellationToken cancellationToken = default);
        Task StopAsync(CancellationToken cancellationToken = default);
    }
}
