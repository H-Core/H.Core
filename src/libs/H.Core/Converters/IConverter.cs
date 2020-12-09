using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Converters
{
    public interface IConverter : IModule
    {
        bool IsStreamingRecognitionSupported { get; }

        Task<string> ConvertAsync(byte[] bytes, CancellationToken cancellationToken = default);
        Task<IStreamingRecognition> StartStreamingRecognitionAsync(CancellationToken cancellationToken = default);
    }
}
