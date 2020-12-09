using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Synthesizers
{
    public interface ISynthesizer : IModule
    {
        bool UseCache { get; set; }

        Task<byte[]> ConvertAsync(string text, CancellationToken cancellationToken = default);
    }
}
