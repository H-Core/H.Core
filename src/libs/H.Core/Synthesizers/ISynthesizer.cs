using System.Collections.Generic;
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

        /// <summary>
        /// 
        /// </summary>
        ICollection<AudioSettings> SupportedSettings { get; }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="settings"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<byte[]> ConvertAsync(string text, AudioSettings? settings = null, CancellationToken cancellationToken = default);

        #endregion
    }
}
