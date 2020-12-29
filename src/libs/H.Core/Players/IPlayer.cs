using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Players
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPlayer : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="format"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task PlayAsync(byte[] bytes, AudioFormat format, CancellationToken cancellationToken = default);
    }
}
