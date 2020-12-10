using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Converters
{
    public interface IStreamingRecognition : IDisposable
    {
        event EventHandler<string>? PartialResultsReceived;
        event EventHandler<string>? FinalResultsReceived;

        Task WriteAsync(byte[] bytes, CancellationToken cancellationToken = default);
        Task StopAsync(CancellationToken cancellationToken = default);
    }
}
