using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Synthesizers
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISynthesizer : IModule
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        bool UseCache { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="format"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<byte[]> ConvertAsync(string text, AudioFormat format, CancellationToken cancellationToken = default);

        #endregion
    }
}
