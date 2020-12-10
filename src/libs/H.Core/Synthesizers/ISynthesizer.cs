using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Synthesizers
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISynthesizer : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        bool UseCache { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<byte[]> ConvertAsync(string text, CancellationToken cancellationToken = default);
    }
}
